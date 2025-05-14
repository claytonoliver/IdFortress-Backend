# 🛡️ IdFortress - API Antifraude (Sprint 2 - QUOD & FIAP)

Este projeto representa uma aplicação **backend antifraude** desenvolvida em **.NET 8**, com persistência em **MongoDB**, e arquitetura baseada no modelo **C4**. A solução foi desenvolvida como parte do desafio da **Sprint 2** do programa **QUOD & FIAP**.

---

## 🚀 Funcionalidades Implementadas

- ✅ Validação de **biometria facial**
- ✅ Validação de **biometria digital**
- ✅ Validação de **documentos (documentoscopia)**
- ✅ Detecção de fraudes simuladas (ex: deepfake, máscara, foto de foto)
- ✅ Registro de tentativas de fraude e sucessos no MongoDB
- ✅ Notificação de fraude para sistema de monitoramento interno (simulado)

---

## 🧱 Estrutura do Projeto

---

## 🛠️ Tecnologias e Bibliotecas

- **ASP.NET Core 8**
- **MongoDB.Driver** (integração com MongoDB)
- **Swashbuckle.AspNetCore** (Swagger)
- **MongoDB.Bson** (atributos de serialização)
- **System.Text.Json / Newtonsoft.Json**

---

## 🐳 Banco de Dados - MongoDB via Docker

Para executar o banco de dados localmente, você pode utilizar o seguinte comando Docker:

docker run -d -p 27017:27017 --name mongo-test mongo

```bash

"MongoSettings": {
  "ConnectionString": "mongodb://localhost:27017",
  "DataBaseName": "IdFortressDb"
}
