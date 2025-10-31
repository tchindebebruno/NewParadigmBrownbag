---

# **Présentation du nouveau paradigme**

Ce dépôt illustre la mise en œuvre d’un **paradigme orienté agent** pour le développement d’applications prompt-driven.

## **Contenu du dépôt**

- **`n8n.json`**  
  Contient le template du workflow **n8n** mettant en évidence l’architecture d’un agent IA :  
  un chatbot qui reçoit un message, le traite et interagit avec ses outils pour fournir une réponse adaptée.

- **Répertoire `AgentBased`**  
  Projet **.NET 10** développé en **Clean Architecture**, implémentant le nouveau paradigme :  
  - Application orientée **Prompt-Driven Development**  
  - Un agent connecté à des tools, des endpoints et des prompts spécifiques pour chaque endpoint  
  - Architecture optimisée pour un développement **Prompt-First**, basé sur un agent et ses outils

- **Répertoire `SubsManager`**  
  Projet **.NET 10** exposé en **API**, puis en **MCP STDIO** et **MCP HTTP**, permettant au modèle d’utiliser l’application.  
  Ce projet illustre la nouvelle approche où les développeurs peuvent créer des outils et les fournir aux modèles via **Microsoft Agent Framework**.

---

## **Cloner le projet**

```bash
git clone https://github.com/tchindebebruno/NewParadigmBrownbag.git
```

---

## **Démarrage : lancer d’abord le projet SubsManager**

### **Prérequis**
- Créer une base de données **PostgreSQL**
- Configurer la chaîne de connexion dans :  
  `SubsManager.McpStdio/Properties/launchSettings.json`

### **Installation**
```bash
cd SubsManager.McpStdio
dotnet restore .
```

### **Build**
```bash
dotnet build .
```

### **Exécution**
```bash
dotnet run
```
Le service sera exposé en **SSE** sur le port **5000** :  
http://localhost:5000/sse

---

## **Configurer et lancer le projet AgentBased**

### **Prérequis**
- Configurer l’agent :  
  - Modifier le modèle OpenAI (ou autre) dans :  
    `AgentBased.Infrastructure/DependencyInjection.cs`
- Si vous utilisez OpenAI :  
  - Mettre à jour la clé `OpenAI__ApiKey` dans :  
    `AgentBased.Api/Properties/launchSettings.json` ou `appsettings.json`

### **Installation**
```bash
cd AgentBased.Api
dotnet restore .
```

### **Build**
```bash
dotnet build .
```

### **Exécution**
```bash
dotnet run
```

---

**Astuce** : Assurez-vous que **SubsManager** est actif avant de démarrer **AgentBased**, afin que l’agent puisse utiliser le MCP exposé.

---
