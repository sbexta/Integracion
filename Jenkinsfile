pipeline {
    agent any

    environment {
        COMPOSE_PROJECT_NAME = "miproyecto-pipeline"
    }

    stages {
        stage('Clonar repositorio') {
            steps {
                checkout scm
            }
        }

        stage('Construir contenedores') {
            steps {
                sh 'docker compose build --no-cache'
            }
        }

        stage('Verificar archivos en contenedor') {
            steps {
               sh 'docker compose run --rm --entrypoint "" web find /app -name "*.dll"'
            }
        }

        stage('Desplegar') {
            when {
                expression { currentBuild.currentResult == 'SUCCESS' }
            }
            steps {
                echo 'Desplegando aplicación...'
                // Agrega aquí el script de despliegue, si aplica
            }
        }
    }

    post {
        success {
            mail to: 'destinatario@ejemplo.com',
                 subject: "✅ Pipeline exitoso: ${env.JOB_NAME} #${env.BUILD_NUMBER}",
                 body: """Hola,

El pipeline '${env.JOB_NAME}' se ejecutó correctamente.

Detalles:
- Proyecto: ${env.JOB_NAME}
- Ejecución: #${env.BUILD_NUMBER}
- URL: ${env.BUILD_URL}

Saludos,
Jenkins"""
        }

        always {
            echo "Limpieza de contenedores temporales si es necesario."
            sh 'docker compose down --volumes --remove-orphans || true'
        }
    }
}

