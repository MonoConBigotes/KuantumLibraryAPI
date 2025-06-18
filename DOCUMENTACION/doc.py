from reportlab.lib.pagesizes import letter
from reportlab.platypus import SimpleDocTemplate, Paragraph, Spacer, PageBreak
from reportlab.lib.styles import getSampleStyleSheet, ParagraphStyle
from reportlab.lib.enums import TA_JUSTIFY, TA_LEFT, TA_CENTER
from reportlab.lib.units import inch

def generate_deployment_pdf(filename="KuantumLibraryAPI_Deployment_Guide.pdf"):
    doc = SimpleDocTemplate(filename, pagesize=letter,
                            rightMargin=72, leftMargin=72,
                            topMargin=72, bottomMargin=18)
    styles = getSampleStyleSheet()

    # Custom styles
    h1_style = ParagraphStyle('H1', parent=styles['h1'], alignment=TA_CENTER, spaceAfter=20)
    h2_style = ParagraphStyle('H2', parent=styles['h2'], spaceBefore=12, spaceAfter=6)
    h3_style = ParagraphStyle('H3', parent=styles['h3'], spaceBefore=10, spaceAfter=4, leftIndent=12)
    body_style = ParagraphStyle('BodyText', parent=styles['Normal'], spaceAfter=6, alignment=TA_JUSTIFY)
    code_style = ParagraphStyle(
        'CodeBox',
        parent=styles['Code'],
        leftIndent=15,
        rightIndent=15,
        spaceBefore=6,
        spaceAfter=12,
        fontSize=8,
        leading=11,
        backColor='#F0F0F0',
        borderColor='#CCCCCC',
        borderWidth=0.5,
        borderPadding=6,
        firstLineIndent=0)
    bullet_style = ParagraphStyle('Bullet', parent=styles['Bullet'], leftIndent=20, spaceAfter=2)

    story = []

    # Title
    story.append(Paragraph("Guía de Despliegue KuantumLibraryAPI", h1_style))
    story.append(Spacer(1, 0.5*inch))

    # --- Content ---
    content = [
        ("1. Prerrequisitos:", h2_style),
        ("""
        Asegúrate de tener el siguiente software instalado:
        """, body_style),
        ("- <b>SDK de .NET 8</b>: Necesario para compilar la aplicación. Descargar desde <a href='https://dotnet.microsoft.com/download/dotnet/8.0' color='blue'><u>https://dotnet.microsoft.com/download/dotnet/8.0</u></a>.", bullet_style),
        ("- <b>Runtime de .NET 8</b>: Necesario para ejecutar la aplicación en el servidor de despliegue (ASP.NET Core Runtime).", bullet_style),
        ("- <b>Servidor MySQL/MariaDB</b>: Versión 8.0.23 o compatible.", bullet_style),
        ("- <b>Git (Opcional)</b>: Para clonar el repositorio si el despliegue se hace desde el control de fuentes.", bullet_style),

        ("2. Configuración de la Base de Datos", h2_style),
        ("<b>2.1. Creación de la Base de Datos y Tablas</b>", h3_style),
        ("""
        Ejecuta el script <code>ScriptDB.sql</code> para crear la base de datos <code>library_db</code> y las tablas necesarias.
        El script se encuentra en: <code>...\\KuantumLibraryAPI\\DOCUMENTACION\\ScriptDB.sql</code>.
        """, body_style),
        ("""
-- Ejemplo de conexión a MySQL/MariaDB y ejecución del script (ajusta según tu cliente MySQL/MariaDB):
-- mysql -u tu_usuario_db -p
-- mysql> SOURCE ...\\KuantumLibraryAPI\\DOCUMENTACION\\ScriptDB.sql;
        """, code_style),
        ("<b>2.2. Configuración del String de Conexión</b>", h3_style),
        ("""
        La API necesita saber cómo conectarse a la base de datos. Esto se configura en el archivo <code>appsettings.json</code> (o <code>appsettings.Production.json</code> para entornos de producción) dentro del directorio de la aplicación publicada.
        Modifica la sección <code>ConnectionStrings</code>:
        """, body_style),
        ("""
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tu_servidor_mysql;Port=3306;Database=library_db;Uid=tu_usuario_db;Pwd=tu_contraseña_db;"
  }
  // ... otras configuraciones ...
}
        """, code_style),
        ("<b>Nota:</b> Reemplaza <code>tu_servidor_mysql</code>, <code>tu_usuario_db</code>, y <code>tu_contraseña_db</code> con tus credenciales.", body_style),

        ("3. Instalación de Dependencias", h2_style),
        ("""
        Antes de compilar, asegúrate de restaurar todas las dependencias del proyecto. Navega al directorio raíz del proyecto (<code>...\\KuantumLibraryAPI\\</code>) en una terminal y ejecuta:
        """, body_style),
        ("dotnet restore", code_style),
        
        # Marcador para el salto de página
        ("PAGE_BREAK", None),
        
        ("4. Compilación y Publicación de la Aplicación", h2_style),
        ("""
        Para preparar la aplicación para el despliegue, compílala y publícala. Desde el directorio raíz del proyecto:
        """, body_style),
        ("<b>4.1. Compilar (Opcional, ya que <code>publish</code> lo incluye):</b>", h3_style),
        ("dotnet clean (para este caso en particular, se usó este comando)", code_style),
        ("dotnet restore (para este caso en particular, se usó este comando)", code_style),
        ("dotnet build (para este caso en particular, se usó este comando)", code_style),
        ("dotnet build -c Release", code_style),
        ("<b>4.2. Publicar la aplicación:</b>", h3_style),
        ("dotnet publish -c Release -o ./publish_output", code_style),
        ("""
        Esto compilará la aplicación en modo <code>Release</code> y colocará los archivos necesarios en la carpeta <code>publish_output</code>.
        Copia el contenido de esta carpeta al servidor donde se ejecutará la API.
        """, body_style),

        ("5. Acceso a la API", h2_style),
        ("""
        Una vez que los archivos publicados estén en el servidor y la aplicación se haya iniciado (por ejemplo, ejecutando <code>dotnet KuantumLibraryApi.dll</code> desde la carpeta de publicación), puedes acceder a la API.
        Para explorar los endpoints y probar la API, utiliza la interfaz de Swagger UI (si está habilitada para el entorno de desarrollo o según tu configuración):
        """, body_style),
        ("http://localhost:5000/swagger", code_style),
        ("""
        Asegúrate de que el puerto (<code>5000</code> en el ejemplo) coincida con el puerto en el que Kestrel está escuchando.
        Los endpoints de la API estarán disponibles bajo <code>http://localhost:5000/api/documents (para este caso)</code>
        """, body_style),

        ("6. Verificación Post-Despliegue", h2_style),
        ("""
        Después de desplegar y ejecutar la API:
        """, body_style),
        ("- <b>Revisa los logs de la aplicación:</b> Busca errores de inicio o de conexión a la base de datos. Los logs se mostrarán en la consola si ejecutas <code>dotnet KuantumLibraryApi.dll</code> directamente.", bullet_style),
        ("- <b>Accede a Swagger UI:</b> Abre <code>http://localhost:5000/swagger</code> en tu navegador para verificar que la interfaz carga y muestra los endpoints.", bullet_style),
        ("- <b>Prueba los endpoints:</b> Realiza algunas llamadas a la API a través de Swagger UI o una herramienta como Postman para asegurar que las operaciones CRUD funcionan correctamente con la base de datos.", bullet_style),
    ]

    for item in content:
        if isinstance(item, tuple):
            text, style = item
            if text == "PAGE_BREAK":
                story.append(PageBreak())
                continue
                
            if style == bullet_style and "\n" in text:
                lines = text.split('\n')
                for line in lines:
                    if line.strip():
                        story.append(Paragraph(line.strip(), style))
            else:
                story.append(Paragraph(text, style))
            
            if style in [h1_style, h2_style, h3_style]:
                story.append(Spacer(1, 0.1*inch))

    doc.build(story)
    print(f"PDF '{filename}' generado exitosamente.")

if __name__ == '__main__':
    generate_deployment_pdf()