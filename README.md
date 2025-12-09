🏢 Multi-Tenant RAG Chatbot Platform (.NET 9 | Qdrant | MongoDB | Ollama)

A powerful multi-tenant Retrieval-Augmented Generation (RAG) chatbot platform where organizations and individuals can create their own isolated AI chatbots.
Each chatbot supports document uploads, multiple LLMs, custom API keys, and fully isolated vector search, enabling secure and high-performance AI chat experiences.

🚀 Key Features
🔹 Multi-Tenant Architecture

Create organizations and chatbots under them.

Each chatbot is fully isolated using unique chatBotId.

API keys are generated per chatbot using UUID for secure integration.

🔹 RAG Pipeline

Users can upload PDF and TXT files.

Files are chunked in the service layer.

Ollama embedding models generate vector embeddings.

Embeddings stored in a Qdrant vector database.

Vector similarity search (cosine similarity) retrieves top-5 relevant chunks.

Retrieved chunks are passed into the selected LLM for grounded responses.

🔹 Multiple LLM Options

Users can choose from powerful models:

GptOss120B

GptOss20B

KimiK2

Llama4Scout

Each request supports configurable:

temperature

topP

maxTokens

🔹 Database Architecture
🗄️ Relational Database (RDBMS)

Stores:

Users

Organizations

Chatbots

API Keys

System metadata

Includes transaction handling to maintain consistency.

🍃 MongoDB

Stores:

User prompts

System-generated prompts

Conversation history

This ensures scalability for large text data.

🔹 Vector Search with Qdrant

Embeddings stored with chatbot isolation (chatBotId).

Cosine similarity used to rank top results.

Results merged into LLM prompt to provide grounded answers.

🔹 File Storage

Uploaded PDFs/TXT files stored in a user-based folder structure.

Each organization/chatbot has its own directory.

🔹 Design Patterns Used

Implemented for clean and scalable architecture:

Strategy Pattern → Easily support new document types (PDF/TXT/others).

Observer Pattern → Internal event handling.

Architecture supports easily adding:

new LLM providers

new document processors

new RAG features

🔹 Tech Stack

.NET Core 9 (Backend)

Ollama (embedding model)

Qdrant (vector DB)

MongoDB

Relational DB (PostgreSQL/MySQL/SQLServer)

Docker support

🧩 How It Works (High-Level Flow)

User uploads documents (PDF/TXT).

Backend extracts text → chunks → embedding generation.

Embeddings saved in Qdrant with chatbot isolation.

User sends query (via API or UI).

Cosine similarity search retrieves top 5 relevant chunks.

Chunks + user query passed to selected LLM (GptOss, KimiK2, Llama4Scout).

LLM generates final grounded answer.

Prompts & conversations stored in MongoDB.

🔐 API Keys & Security

Each chatbot receives a unique UUID-based API key.

API key must be included in request headers.

Backend validates key → resolves organization + chatbot.

📁 Project Capabilities

Multi-tenant RAG chatbot creation.

Document upload & storage.

Embedding generation and vector search.

Multiple LLM support.

Customizable LLM parameters.

Strong database layer with RDB + Mongo hybrid design.

Production-ready architecture with patterns for extensibility.

🧱 Future Enhancements

Add support for more document types.

Plug-and-play additional LLMs and embedding models.

Extend to streaming chat responses.

Enable admin dashboards for usage analytics.
