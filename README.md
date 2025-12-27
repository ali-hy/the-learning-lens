# The Learning Lens: Immersive Training Suite

> **Transforming vocational training with Virtual Reality, Mixed Reality, and AI-driven Evaluation.**

## 📖 Project Overview
**The Learning Lens** is a graduation project designed to revolutionize how industrial assembly skills are taught and assessed. In traditional settings, training beginners on complex machinery (like motorcycles) is expensive, dangerous, and requires constant human supervision. 

Our solution bridges this gap by creating a safe, immersive **Virtual Reality (VR)** environment for learning and a **Mixed Reality (AR)** mode for real-world assessment. The system uniquely integrates an **AI Vision Module** that can grade a user's physical assembly quality from a single photo, eliminating the need for a human supervisor.

### 🌟 Key Features
* **Dual-Mode Training:**
    * **VR Mode (Meta Quest 2/3):** A fully immersive workshop where users learn to assemble complex 3D models (Motorcycles, Wardrobes) with haptic feedback, snapping mechanics, and step-by-step guidance.
    * **MR Mode (Meta Quest 3):** Uses passthrough technology to guide users in assembling physical objects (e.g., LEGO models) in the real world.
* **AI-Powered Assessment:** A custom Python backend using **LPIPS** (Learned Perceptual Image Patch Similarity) to detect errors—such as missing parts—with human-like perception.
* **Smart Backend:** An ASP.NET Core system that tracks user progress, manages lessons, and stores performance metrics in SQL Server.
* **Gamified Learning:** Includes "Lesson Mode" for guided tutorials and "Test Mode" for high-pressure, unassisted evaluation.

## 🛠️ Technical Architecture
The system follows a scalable 3-Tier Architecture:

1.  **Presentation Layer (Unity):** Handles XR interactions (VR/AR), utilizing the **Meta XR Core SDK** and **XR Interaction Toolkit** for cross-platform support.
2.  **Logic Layer (ASP.NET Core):** Manages authentication (Identity), business logic, and serves a RESTful API.
3.  **Data Layer (SQL Server):** Uses Entity Framework Core for persisting user profiles, lesson data, and assessment scores.

### 🤖 AI Evaluation Module: The LPIPS Pivot
One of our biggest technical challenges was grading the AR builds. We initially attempted **3D Reconstruction (Pix2Vox)** to recreate the user's object in 3D, but it proved unreliable for small, detailed parts.

**The Solution:** We pivoted to a vision-based approach using **OpenCV** and **LPIPS**. Unlike traditional metrics like SSIM (which failed due to lighting changes), LPIPS uses deep learning features to "perceive" the image.
* **Result:** The system can accurately identify a missing headlight or misplaced brick while ignoring environmental noise like shadows or slight camera shifts.

| Metric | SSIM (Traditional) | LPIPS (Our Solution) |
| :--- | :--- | :--- |
| **Sensitivity** | Extremely High (False Negatives) | Human-like Perception |
| **Robustness** | Fails with slight camera shifts | Robust to minor misalignments |
| **Verdict** | ❌ Unsuitable for AR | ✅ Adopted for Project |

## 🚧 Project Status & Integration Note
**Current Status:** Functional Proof of Concept (PoC).

The Learning Lens consists of three fully developed standalone modules:
1.  The **Unity VR/AR Frontend** (Fully Functional).
2.  The **ASP.NET Backend & Database** (Fully Functional).
3.  The **Python AI Evaluation Script** (Fully Functional).

> **⚠️ Integration Disclaimer:** For the purpose of our final demonstration, the real-time "bridge" between the Meta Quest 3 client and the Python evaluation script is simulated. While both systems work perfectly independently, the automated pipeline triggering the script from the headset is currently mocked with placeholder triggers. Future work includes finalizing this API handshake for a seamless end-to-end experience.

## 📊 Performance & Results
We conducted on-campus user testing with real students. The system received a **System Usability Scale (SUS) score of 74.8%**, categorizing the user experience as **"Very Good"**. 

**UEQ (User Experience Questionnaire) Highlights:**
* **Efficiency:** 0.86 (High efficiency in task completion)
* **Attractiveness:** 0.70 (Users found the interface visually appealing)
* **Novelty:** 0.70 (Users found the concept innovative)

## 🎥 Video Demos
Watch the full demonstrations of the VR Motorcycle, Wardrobe, and Mixed Reality modes here:

[**📺 Watch the Full YouTube Playlist**](https://www.youtube.com/playlist?list=PLxwu6CspzTKTWZwrkIClWTBr3Si2u0tnv)

## 🚀 Installation Guide
To install and use the VR/AR Educational System on your Meta Quest, follow the instructions below to sideload the application.

### Prerequisites
* **Meta Quest 3** (Recommended for AR/VR) or **Meta Quest 2** (VR Only).
* **Meta Quest Developer Hub (MQDH)** or **SideQuest** installed on your PC.
* USB-C Cable for connection.

### Installation Steps

**Step 1: Obtain the APK**
Since there is no pre-built release, clone this repository and build the project using Unity to generate the `.apk` file.

**Step 2: Enable Developer Mode on Quest**
1.  Open the **Meta Quest mobile app** on your phone.
2.  Go to **Devices > Developer Mode** and toggle it **ON**.
3.  Connect your Quest to your PC via USB and approve any authorization prompts inside the headset.

**Step 3: Install the APK (Sideloading)**
You can use either **MQDH** or **SideQuest**:
* **Option A (MQDH):** Launch Meta Quest Developer Hub, select your device, and drag-and-drop the generated `.apk` file into the hub.
* **Option B (SideQuest):** Launch SideQuest, click the "Install APK file from folder" icon (top bar), and select your `.apk`.

**Step 4: Launch the App**
1.  Put on your headset.
2.  Open the **App Library**.
3.  Click the search bar/filter and switch to **"Unknown Sources"**.
4.  Locate and launch **The Learning Lens**.

## 👥 The Team
This project was built by a dedicated team of seniors from the Faculty of Computer and Information Sciences, Ain Shams University.

* **Mahmoud Hany Mazroa**
* **Ali Haitham Youssef**
* **Karen Walid Magdy**
* **Mohamed Shokry Mostafa**
* **Mahmoud Sayed Abdein**

### 🎓 Supervision
* **Supervisor:** Prof./Dr. Sherin Rady
* **Technical Guidance:** TA. Yomna Ahmed

## 🔗 Repositories
* **VR Application (Main):** [https://github.com/ali-hy/the-learning-lens](https://github.com/ali-hy/the-learning-lens)
* **AR Application:** [https://github.com/Shokryy/the-learning-lens-ar](https://github.com/Shokryy/the-learning-lens-ar)
