# The Learning Lens: Generic XR Assembly Training Framework

> **A modular framework for procedural skill acquisition—transforming assembly training in Industry, Healthcare, and Education via Virtual & Mixed Reality.**


### 📚 Project Resources
* **📄 [Full Documentation (100-page PDF)](The_Learning_Lens_Documentation.pdf)** – *Deep dive into architecture, research, and testing.*
* **📊 [Project Presentation (Canva Slides)](https://www.canva.com/design/DAGrz5shPyM/mQjDjJBTeRXanVuNYnrj-g/view)** – *High-level overview, visual pitch, and business potential.*

---

## 📖 Project Overview
[cite_start]**The Learning Lens** is a graduation project designed to revolutionize how procedural assembly tasks are taught and assessed. [cite: 31, 32] [cite_start]In traditional settings, training beginners on complex machinery (like motorcycles) is expensive, dangerous, and requires constant human supervision. [cite: 122, 123]

We created a **generic XR framework** capable of training users on *any* assembly sequence. Whether it is a **mechanic assembling an engine**, a **medical student assembling a surgical kit**, or a **child building a LEGO set**, the system adapts to the content. [cite_start]It uniquely integrates an **AI Vision Module** that can grade a user's real-world assembly quality from a single photo, eliminating the need for a human supervisor. [cite: 156, 163]

### 🌟 Key Features
* **Universal Assembly Framework:**
    * [cite_start]Designed as a modular platform where training content (3D models, step-by-step logic) is decoupled from the core system. [cite: 170]
    * [cite_start]**Proven Use Cases:** We demonstrated this versatility by implementing modules for **Automotive Assembly (Motorcycle)**, **Carpentry (Wardrobe)**, and **Gamified STEM Training (LEGO)**. [cite: 36, 163]
* **Dual-Mode Training:**
    * [cite_start]**VR Mode (Meta Quest 2/3):** A fully immersive environment for safe, repeatable practice of complex assembly tasks with haptic feedback and guidance. [cite: 527, 859]
    * [cite_start]**MR Mode (Meta Quest 3):** Uses passthrough technology to overlay digital guides onto the real world, allowing users to practice assembly with physical objects. [cite: 951, 954]
* [cite_start]**AI-Powered Assessment:** A custom Python backend using **LPIPS** (Learned Perceptual Image Patch Similarity) to detect assembly errors—such as missing parts—with human-like perception. [cite: 1048, 1057]
* [cite_start]**Smart Backend:** An ASP.NET Core system that tracks user progress across different assembly domains and stores performance metrics in SQL Server. [cite: 512, 188]

## 🛠️ Technical Architecture
[cite_start]The system follows a scalable 3-Tier Architecture[cite: 486, 504]:

1.  [cite_start]**Presentation Layer (Unity):** Handles XR interactions (VR/AR), utilizing the **Meta XR Core SDK** and **XR Interaction Toolkit** for cross-platform support. [cite: 947, 950]
2.  [cite_start]**Logic Layer (ASP.NET Core):** Manages authentication (Identity), generic lesson logic, and serves a RESTful API. [cite: 961, 965]
3.  [cite_start]**Data Layer (SQL Server):** Uses Entity Framework Core for persisting user profiles, generic lesson data, and assessment scores. [cite: 987]

### 🤖 AI Evaluation Module: The LPIPS Pivot
One of our biggest technical challenges was grading real-world builds without human intervention. [cite_start]We initially attempted **3D Reconstruction (Pix2Vox)** but found it unreliable for detailed parts. [cite: 1009, 1018]

**The Solution:** We pivoted to a vision-based approach using **OpenCV** and **LPIPS**. [cite_start]Unlike traditional metrics like SSIM, LPIPS uses deep learning features to "perceive" the image. [cite: 1057, 1058]
* [cite_start]**Result:** The system accurately identified missing parts (like a LEGO headlight) while ignoring environmental noise, proving the viability of automated grading for physical assembly tasks. [cite: 1071]

| Metric | SSIM (Traditional) | LPIPS (Our Solution) |
| :--- | :--- | :--- |
| **Sensitivity** | [cite_start]Extremely High (False Negatives) [cite: 1066] | [cite_start]Human-like Perception [cite: 1058] |
| **Robustness** | [cite_start]Fails with slight camera shifts [cite: 1070] | [cite_start]Robust to minor misalignments [cite: 1072] |
| **Verdict** | ❌ Unsuitable for AR | ✅ Adopted for Project |

## 🚧 Project Status & Integration Note
**Current Status:** Functional Proof of Concept (PoC).

The Learning Lens consists of three fully developed standalone modules:
1.  The **Unity VR/AR Frontend** (Fully Functional).
2.  The **ASP.NET Backend & Database** (Fully Functional).
3.  The **Python AI Evaluation Script** (Fully Functional).

> **⚠️ Integration Disclaimer:** For the purpose of our final demonstration, the real-time "bridge" between the Meta Quest 3 client and the Python evaluation script is simulated. While both systems work perfectly independently, the automated pipeline triggering the script from the headset is currently mocked with placeholder triggers. Future work includes finalizing this API handshake for a seamless end-to-end experience.

## 📊 Performance & Results
We conducted on-campus user testing with real students. [cite_start]The system received a **System Usability Scale (SUS) score of 74.8%**, categorizing the user experience as **"Very Good"**. [cite: 43]

**UEQ (User Experience Questionnaire) Highlights:**
* [cite_start]**Efficiency:** 0.86 (High efficiency in task completion) [cite: 43]
* [cite_start]**Attractiveness:** 0.70 (Users found the interface visually appealing) [cite: 43]
* [cite_start]**Novelty:** 0.70 (Users found the concept innovative) [cite: 43]

## 🎥 Video Demos
Watch the full demonstrations of the platform applied to different assembly domains:

[**📺 Watch the Full YouTube Playlist**](https://www.youtube.com/playlist?list=PLxwu6CspzTKTWZwrkIClWTBr3Si2u0tnv)

## 🚀 Installation Guide
[cite_start]To install and use the VR/AR Educational System on your Meta Quest, follow the instructions below to sideload the application. [cite: 1076]

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
3.  [cite_start]Connect your Quest to your PC via USB and approve any authorization prompts inside the headset. [cite: 1078, 1079]

**Step 3: Install the APK (Sideloading)**
You can use either **MQDH** or **SideQuest**:
* [cite_start]**Option A (MQDH):** Launch Meta Quest Developer Hub, select your device, and drag-and-drop the generated `.apk` file into the hub. [cite: 1082]
* [cite_start]**Option B (SideQuest):** Launch SideQuest, click the "Install APK file from folder" icon (top bar), and select your `.apk`. [cite: 1084]

**Step 4: Launch the App**
1.  Put on your headset.
2.  Open the **App Library**.
3.  Click the search bar/filter and switch to **"Unknown Sources"**.
4.  [cite_start]Locate and launch **The Learning Lens**. [cite: 1087]

## 👥 The Team
[cite_start]This project was built by a dedicated team of seniors from the Faculty of Computer and Information Sciences, Ain Shams University. [cite: 1, 2, 7]

* **Mahmoud Hany Mazroa**
* **Ali Haitham Youssef**
* **Karen Walid Magdy**
* **Mohamed Shokry Mostafa**
* **Mahmoud Sayed Abdein**

### 🎓 Supervision
* **Supervisor:** Prof./Dr. [cite_start]Sherin Rady [cite: 8]
* **Technical Guidance:** TA. [cite_start]Yomna Ahmed [cite: 11]

## 🔗 Repositories
* **VR Application (Main):** [https://github.com/ali-hy/the-learning-lens](https://github.com/ali-hy/the-learning-lens)
* **AR Application:** [https://github.com/Shokryy/the-learning-lens-ar](https://github.com/Shokryy/the-learning-lens-ar)
