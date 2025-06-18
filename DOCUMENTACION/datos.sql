-- Documento 1 (Libro de física cuántica)
INSERT INTO documents (name, description, author_full_name, author_email, serial_code, publication_code, created_at)
VALUES (
  'Principios de Mecánica Cuántica',
  'Texto fundamental para entender los principios básicos de la cuántica',
  'Niels Bohr',
  'bohr@copenhagen.edu',
  'BOHR123456789',
  'ISO-1922',
  NOW()
);

-- Documento 2 (Artículo científico)
INSERT INTO documents (name, description, author_full_name, author_email, serial_code, publication_code, created_at)
VALUES (
  'El Gato de Schrödinger: Paradojas Cuánticas',
  'Análisis de la famosa paradoja del gato vivo/muerto',
  'Erwin Schrödinger',
  'schrodinger@vienna.edu',
  'SCHR987654321',
  'P-08.19350215',  -- Formato P-XX.YYYYMMDD
  NOW()
);

-- Documento 3 (Normativa técnica)
INSERT INTO documents (name, description, author_full_name, author_email, serial_code, publication_code, created_at)
VALUES (
  'Estándares para Computación Cuántica',
  'Normativas ISO para desarrollo de hardware cuántico',
  'Quantum Standards Body',
  'standards@quantum.org',
  'QSTD456789123',
  'ISO-2023',
  NOW()
);

-- Documento 4 (Ley regulatoria)
INSERT INTO documents (name, description, author_full_name, author_email, serial_code, publication_code, created_at)
VALUES (
  'Ley de Seguridad Cuántica Nacional',
  'Regulaciones para protección de sistemas criptográficos',
  'Gobierno Federal',
  'contacto@presidencia.gov',
  'LEY654321987',
  'Ley N° 25.678',  -- Formato con separador de miles
  NOW()
);

-- Documento 5 (Tesis doctoral)
INSERT INTO documents (name, description, author_full_name, author_email, serial_code, publication_code, created_at)
VALUES (
  'Entrelazamiento Cuántico en Sistemas Macroscópicos',
  'Investigación pionera sobre entrelazamiento a gran escala',
  'Juan Pérez',
  'j.perez@mit.edu',
  'PEREZ789123456',
  'TH-01.20240601',  -- TH = Tesis
  NOW()
);

-- Índices para Documento 1
INSERT INTO document_page_indices (document_id, name, page, created_at)
VALUES 
  (1, 'Prefacio', 1, NOW()),
  (1, 'Dualidad Onda-Partícula', 25, NOW()),
  (1, 'Principio de Incertidumbre', 50, NOW()),
  (1, 'Ecuación de Schrödinger', 75, NOW());

-- Índices para Documento 2
INSERT INTO document_page_indices (document_id, name, page, created_at)
VALUES 
  (2, 'Introducción', 1, NOW()),
  (2, 'La Paradoja Original', 15, NOW()),
  (2, 'Interpretaciones Modernas', 30, NOW());

-- Índices para Documento 3
INSERT INTO document_page_indices (document_id, name, page, created_at)
VALUES 
  (3, 'Ámbito de Aplicación', 1, NOW()),
  (3, 'Qubits Estándar', 42, NOW()),
  (3, 'Métricas de Coherencia', 88, NOW()),
  (3, 'Certificación', 120, NOW());

-- Índices para Documento 4
INSERT INTO document_page_indices (document_id, name, page, created_at)
VALUES 
  (4, 'Artículo 1: Definiciones', 1, NOW()),
  (4, 'Artículo 5: Sanciones', 12, NOW());

-- Índices para Documento 5
INSERT INTO document_page_indices (document_id, name, page, created_at)
VALUES 
  (5, 'Resumen Ejecutivo', 1, NOW()),
  (5, 'Metodología', 30, NOW()),
  (5, 'Resultados Experimentales', 60, NOW()),
  (5, 'Conclusiones', 95, NOW()),
  (5, 'Bibliografía', 100, NOW());