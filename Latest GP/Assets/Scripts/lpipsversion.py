import os
import cv2
import torch
import lpips
import numpy as np
import matplotlib.pyplot as plt
import sys
from torchvision import transforms

# --- CONFIGURATION ---
NROWS = 7
NCOLS = 10
DEVICE = torch.device("cuda" if torch.cuda.is_available() else "cpu")
# USER_IMAGE_PATH = r"C:\Users\mahmo\Desktop\Code\GP\Shokry USER IMAGES\IMG-20250629-WA0090.jpg"
USER_IMAGE_PATH = sys.argv[1]
# TEST_IMAGES_BASE_DIR = r"C:\Users\mahmo\Desktop\Code\GP\Shokry REF IMAGES"
TEST_IMAGES_BASE_DIR = sys.argv[2]
HEATMAP_DIR = sys.argv[3]
# HEATMAP_DIR = None

# --- LOAD LPIPS MODEL ---
lpips_model = lpips.LPIPS(net='alex').to(DEVICE)
lpips_model.eval()

# --- LOAD IMAGES ---
USER_IMAGE = cv2.imread(USER_IMAGE_PATH, cv2.IMREAD_GRAYSCALE)
test_images_names = os.listdir(TEST_IMAGES_BASE_DIR)
test_images_full_paths = [os.path.join(TEST_IMAGES_BASE_DIR, name) for name in test_images_names]
test_images = [cv2.imread(path, cv2.IMREAD_GRAYSCALE) for path in test_images_full_paths]

# --- UTILITY: PARTITION IMAGE INTO PATCHES ---
def partition_image(img, nrows, ncols):
    h, w = img.shape
    patch_h = h // nrows
    patch_w = w // ncols
    patches = []
    for i in range(nrows):
        for j in range(ncols):
            y1 = i * patch_h
            y2 = h if i == nrows - 1 else (i + 1) * patch_h
            x1 = j * patch_w
            x2 = w if j == ncols - 1 else (j + 1) * patch_w
            patch = img[y1:y2, x1:x2]
            patches.append(patch)
    return patches

# --- CONVERT GRAY PATCH TO TENSOR FOR LPIPS ---
def gray_patch_to_tensor(patch):
    rgb = cv2.cvtColor(patch, cv2.COLOR_GRAY2RGB)
    rgb = rgb.astype(np.float32) / 255.0
    tensor = torch.from_numpy(rgb).permute(2, 0, 1).unsqueeze(0) * 2 - 1
    return tensor.to(DEVICE)

# --- LPIPS SCORE BETWEEN PATCH LISTS ---
def lpips_on_parts(u_img_parts, v_img_parts):
    scores = []
    for i in range(len(u_img_parts)):
        u_tensor = gray_patch_to_tensor(u_img_parts[i])
        v_tensor = gray_patch_to_tensor(v_img_parts[i])
        with torch.no_grad():
            d = lpips_model(u_tensor, v_tensor).item()
        # Invert LPIPS score: lower = better match → map to high score
        scores.append(1 - d)
    return scores

# --- PARTITION ALL ---
u_img_parts = partition_image(USER_IMAGE, NROWS, NCOLS)
v_imgs_parts = [partition_image(img, NROWS, NCOLS) for img in test_images]

# --- COMPUTE SCORES ---
scores = []
for v_img_parts in v_imgs_parts:
    scores.append(lpips_on_parts(u_img_parts, v_img_parts))

# --- FIND BEST MATCH (highest total similarity) ---
def get_max(scores):
    max_idx = -1
    max_sum = -float("inf")
    for i, score_list in enumerate(scores):
        total = sum(score_list)
        if total > max_sum:
            max_idx, max_sum = i, total
    return max_idx, max_sum

max_idx, max_sum = get_max(scores)
print("max idx: ", max_idx)
print("mean similarity score: ", max_sum / (NROWS * NCOLS))

scores_grid = np.array(scores[max_idx]).reshape((NROWS, NCOLS))
def calculate_ratio(arr):
    s = 0
    f = 0
    for i in range(len(arr)):
        print(arr[i])
        s += sum(arr[i])
        f += len(arr[i])
    return s/f
# --- BINARIZE (FIXED THRESHOLD) ---
threshold = 0.93
binary_grid = (scores_grid >= threshold).astype(np.uint8)
ratio = calculate_ratio(binary_grid)
np.set_printoptions(precision=4, suppress=True)
print(scores_grid)
print(binary_grid)
# --- RESIZE HEATMAP TO IMAGE SIZE ---
heatmap = cv2.resize(binary_grid, (USER_IMAGE.shape[1], USER_IMAGE.shape[0]), interpolation=cv2.INTER_NEAREST)

# --- PLOT HEATMAP OVERLAY ---
user_image_norm = USER_IMAGE.astype(np.float32) / 255.0
test_image_norm = test_images[max_idx].astype(np.float32) / 255.0

plt.imshow(user_image_norm, cmap='gray')
plt.imshow(heatmap, cmap='jet', alpha=0.5)
plt.title("LPIPS Patch Similarity Heatmap (Binarized)")
plt.axis('off')
plt.colorbar(label="Similarity ≥ threshold")
HEATMAP_DIR = os.path.join(HEATMAP_DIR, "scores.jpg")
plt.savefig(HEATMAP_DIR)
plt.show()
print(ratio)

# --- VISUAL OVERLAY OF INPUT AND MATCH ---
plt.imshow(user_image_norm, cmap='gray')
plt.imshow(test_image_norm, cmap='gray', alpha=0.5)
plt.title("User Image & Best Match Overlay")
plt.axis('off')
# plt.show()
