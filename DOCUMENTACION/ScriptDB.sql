-- Creación de la base de datos
CREATE DATABASE IF NOT EXISTS library_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Usar la base de datos
USE library_db;

-- Creación de la tabla documents
CREATE TABLE documents (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    description VARCHAR(1000),
    author_full_name VARCHAR(300) NOT NULL,
    author_email VARCHAR(100) NOT NULL,
    serial_code VARCHAR(16) NOT NULL,
    publication_code VARCHAR(100) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NULL ON UPDATE CURRENT_TIMESTAMP,
    deleted_at TIMESTAMP NULL
) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Creación de la tabla document_page_indices
CREATE TABLE document_page_indices (
    id INT AUTO_INCREMENT PRIMARY KEY,
    document_id INT NOT NULL,
    name VARCHAR(100) NOT NULL,
    page INT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (document_id) REFERENCES documents(id) ON DELETE CASCADE
) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Índices para mejorar el rendimiento de búsqueda
CREATE INDEX idx_documents_serial_code ON documents(serial_code);
CREATE INDEX idx_documents_publication_code ON documents(publication_code);
CREATE INDEX idx_documents_author ON documents(author_full_name, author_email);
CREATE INDEX idx_documents_deleted ON documents(deleted_at);