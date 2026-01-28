# 📘 README

## SAE Institute Stuttgart

**Module:** 4FSC0PD002 – Game Programming (K2 / S2 / S3)  
**Student:** Eric Rosenberg  
**Project:** 3D Character Controller (Unity)

---

## 1. Base Module

This project is the submission of **Eric Rosenberg** for the module  
**4FSC0PD002 – Game Programming (K2 / S2 / S3)** at **SAE Institute Stuttgart**.

The project **“3_3D Character Controller”** was developed using the **Unity Game Engine** as a **3D gameplay prototype**.  
The focus lies on a **modular, physics-based and configurable character controller**, including camera control and an interaction system.

All gameplay-relevant values can be adjusted via **configuration assets** without modifying the code directly.

---

## 2. Missing Submission

*(not applicable – all required components are present)*

---

## 3. Multiple Submissions in One Folder

*(not applicable – single project)*

---

## 4. Group Work

*(not applicable – individual work by Eric Rosenberg)*

---

## 5. Feature Overview

### 🎮 Core Player Controller Features (3D)

- free movement in 3D space  
- physics-based movement using **Rigidbody**  
- walking and sprinting  
- ground-based jumping  
- **Coyote Time**  
- **Jump Buffer**  
- mouse & keyboard and controller support  
- separated ground and air movement logic  
- full configuration via the Unity Inspector  

---

### 🧠 Movement, Jump & Look System

**MoveBehaviour**
- ground movement with acceleration and deceleration  
- sprint mechanic  
- air movement with:
  - air control  
  - air braking  
  - smooth direction changes  

**JumpBehaviour**
- ground-based jumping  
- coyote time support  
- jump buffer handling  
- state evaluation via `JumpStateData`

**LookBehaviour**
- horizontal rotation (yaw) via Rigidbody  
- vertical camera rotation (pitch)  
- separate sensitivity for mouse and controller  
- clamped vertical look angles  

---

### 🧱 Ground Detection & Physics

**GroundCheck**
- box-based ground detection using `Physics.OverlapBox`  
- configurable size and offset  
- LayerMask support  
- optional gizmo visualization for debugging  

---

### 🧩 Interaction System

**RaycastTargetProvider**
- raycast-based target detection  
- configurable distance and layer mask  
- clean separation between detection and interaction  

**IInteractable (Interface)**
- unified interaction contract  
- allows easy extension with new interactable objects  

**Example Interactables**
- **WoodBlock** – destructible object  
- **LightSwitch** – toggles a light on/off  
- **Ball** – physics-based ball that can be shot  

ℹ️ **Interaction UI Note**  
Currently, the interaction UI displays the same visual hint for both **keyboard/mouse** and **controller input**.  
An adjustment is planned so that the interaction hint properly reflects the active input device when using a controller.

---

### 🎛️ Configuration

All gameplay-related values are adjustable via configuration assets:

- **MoveConfig**
  - walk and sprint speed  
  - acceleration / deceleration  
  - air control and air brake  
- **JumpConfig**
  - jump force  
  - coyote time duration  
  - jump buffer time  
- **LookConfig**
  - mouse and controller sensitivity  
  - minimum and maximum look angles  

---

### ⌨️ Input System

- Unity Input System  
- supports:
  - mouse & keyboard  
  - gamepad / controller  
- clean separation of:
  - `Update()` (input & look handling)  
  - `FixedUpdate()` (physics logic)  

---

## ⚙️ Technical Details

- **Engine:** Unity (3D)  
- **Language:** C#  
- **Physics:** Unity 3D Physics  
- **Input:** Unity Input System  
- **Platform:** Windows  
- **IDE:** Visual Studio  
- **Architecture:** Modular / SRP-oriented  

---

## 📂 Folder Structure (Excerpt)

```
Assets/
├── PlayerController/
│   ├── Config/
│   ├── Input/
│   ├── Material/
│   └── Prefabs/
│       ├── Environment/
│       │   └── WoodBlockade/
│       ├── Items/
│       ├── Player/
│       ├── Systems/
│       └── UI/
│
├── Scripts/
│   ├── Interfaces/
│   ├── Items/
│   └── TargetProvider/
│
├── Scenes/
├── Resources/
├── Settings/
└── Packages/
```

---

## 🧾 Submission Details

- **Type:** Individual project  
- **Media:** Playable scene with interactive objects  
- **Required File:** README.md  
- **Guidelines followed:** Yes  

---

## 🧠 Summary

This project demonstrates a **clean and extensible 3D character controller** with a strong focus on **physics**, **modularity**, and **maintainability**.  
The architecture allows new interactions, movement mechanics, or gameplay systems to be added without structural refactoring.

---

**Stuttgart, January 2026**  
*© 2026 Eric Rosenberg – SAE Institute Stuttgart*
