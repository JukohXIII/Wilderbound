# Wilderbound
**A high-stakes, skill-based survival dueler focused on spacing and mechanical mastery.**

## 🎯 Vision
Wilderbound is a spiritual successor to the "survival battle royale" niche. It strips away the RNG of modern BRs to focus on the psychological and mechanical battle between players.

### Core Pillars
* **Calculated Melee**: A combat system where positioning and timing are everything. No spamming, only intentional strikes.
* **Predictive Archery**: Arrows are precious and slow. Success requires reading your opponent's soul, not just their hitbox.
* **Environmental Friction**: The cold isn't just a theme; it's a strategic engine. Use fire to survive, but know that every flame is a beacon for your enemies.
* **The "Weight" of Action**: Every swing and every craft has a commitment. Missing a hit or mistiming a fire can be fatal.

---

## 🛠 Development Roadmap (Devlog)

### Phase 1: The Combat Foundation (Local Prototype)
- [ ] **Kinetic Controller**: Movement with weight, acceleration, and friction to support precise spacing.
- [ ] **Melee Core (The Axe)**:
    - Frame-perfect State Machine: Startup -> Active -> Recovery.
    - Hitbox/Hurtbox logic with proper hit-stop feedback.
- [ ] **Ranged Core (The Bow)**:
    - Physical projectile with gravity and lead-time.
    - Arrow retrieval system.
- [ ] **Stamina Management**: Resource-based actions to prevent mindless dodging/attacking.

### Phase 2: Survival & Crafting
- [ ] **Heat Management**: A depleting "Thermal" gauge that impacts performance.
- [ ] **The Beacon (Fireplace)**: Deployable heat source that creates a visual and auditory "ping" for others.
- [ ] **Resource Gathering**: Simple interaction system to harvest wood for arrows and heat.

### Phase 3: Networking
- [ ] Implement Netcode (Unity Netcode for GameObjects or Photon Fusion).
- [ ] Lag compensation for melee interactions.

---

## 🎨 Art Direction
* **Style**: High-legibility Stylized/Low-Poly.
* **Current Theme**: Frozen Wilderness (High contrast characters against snow).

## 💻 Tech Stack
* **Engine**: Unity 2022.3 LTS
* **Modeling**: ProBuilder
* **Language**: C#
