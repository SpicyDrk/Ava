# 🎮 Game Design Document – *Adventure for Biscuits*  

## 1. Core Concept  
You hire cats to go on **biscuit-collecting missions**.  
- Missions take real time (up to ~3 minutes).  
- Missions can require 1–3 cats.  
- Each mission has a **success chance** that changes depending on the number and type of cats assigned.  
- Players can **increase risk** (double the danger) for **3–4x the reward**.  
- The player must balance **safe progress** with **high-risk gambles**.  

Theme is lighthearted and silly: cats are greedy biscuit hunters with quirky personalities.  

---

## 2. Core Loop  
1. **Hire Cats** → Spend biscuits to recruit new cats.  
2. **Assign Cats** → Drag and drop cats onto missions.  
3. **Wait** → Missions take 10s–180s to complete.  
4. **Resolve Mission** → Cats return with biscuits (success) or empty-pawed (failure).  
5. **Spend Rewards** → Hire more cats, unlock upgrades, access harder missions.  

---

## 3. Mechanics  

### Cats  
- Each cat is a unit that can be hired.  
- Cats are **drag-and-droppable** onto missions.  
- Idle cats show in the adventurer panel until assigned.  
- Cats have no stats at first (simple version), but you can expand later (speedy cats, lucky cats, tough cats).  

### Missions  
- Each mission shows:  
  - **Time duration** (10s–180s).  
  - **Base success chance** (e.g., 70%).  
  - **Base reward** (e.g., 10 biscuits).  
- Assigning cats increases success odds.  
- Risk Toggle: "Double Risk" → halves success chance but multiplies rewards 3–4x.  

### Rewards  
- Successful mission: gain biscuits.  
- Failed mission: cats return empty (time wasted).  
- Upgrades allow partial returns or safer odds.  

### Upgrades (examples)  
- **Hire New Cat** – +1 adventurer slot.  
- **Lucky Collar** – +5% success chance.  
- **Biscuit Wagon** – +20% mission reward.  
- **Time Warp Yarn Ball** – Missions complete 20% faster.  

---

## 4. UI / UX  

### Layout (Wireframe Flow)  
- **Top Bar**: Biscuit counter + Buttons for Shop / Missions.  
- **Mission Panel**:  
  - Shows available missions with timers, success chance, risk toggle, and biscuit reward.  
  - Drag cats onto mission slots (1–3 slots).  
  - Odds update dynamically when cats are added/removed.  
- **Cat Roster Panel**:  
  - Idle cats displayed, draggable.  
  - Locked cats show silhouettes until unlocked.  
- **Upgrade Shop Panel**:  
  - List of upgrades purchasable with biscuits.  
- **Log Panel**:  
  - Fun text about cat missions (success/failure).  

---

## 5. Progression  
- Early game: 1–2 cats, short missions (10–30s), small biscuit payouts.  
- Mid game: More cats (4–6), longer missions (1–3 minutes), larger payouts, more risk toggles.  
- Late game: Unlock silly high-stakes missions (e.g., *Steal the Royal Biscuit*, *Space Biscuit Voyage*).  
- Optional Prestige: Reset progress for “Golden Biscuits” → permanent boosts.  

---

## 6. Aesthetic / Tone  
- Lighthearted, cozy, and funny.  
- Cartoonish cats with unique personalities (e.g., *Sir Whiskers*, *Meowgician*, *Crumbs*).  
- Missions are whimsical biscuit heists and adventures.  
- Failure messages are comedic: *“Mr. Muffins got distracted by a yarn ball… no biscuits this time.”*  

---

## 7. Scope (Jam Feasible Features)  
**Must-Have**  
- Core biscuit counter.  
- Hire cats (resource sink).  
- Missions with drag/drop cats.  
- Success/failure odds + timer system.  
- Rewards loop working.  

**Should-Have**  
- Risk toggle mechanic.  
- Upgrade shop with at least 3 meaningful upgrades.  
- Fun log messages.  

**Could-Have**  
- Cat personalities/stats.  
- Unlockable new zones.  
- Prestige (Golden Biscuits).  

**Won’t-Have (for jam)**  
- Multiplayer.  
- Complex graphics/animations.  
- Deep story.  
