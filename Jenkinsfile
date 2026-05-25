pipeline {
    agent any

    stages {
        stage('Restaurar dependencias') {
            steps {
                bat 'dotnet restore EstoqueApp/EstoqueApp.csproj'
            }
        }

        stage('Compilar projeto') {
            steps {
                bat 'dotnet build EstoqueApp/EstoqueApp.csproj -c Release --no-restore'
            }
        }

        stage('Publicar executavel') {
            steps {
                bat 'dotnet publish EstoqueApp/EstoqueApp.csproj -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true -o publish'
            }
        }

        stage('Gerar instalador') {
            steps {
                bat '"C:\\Program Files (x86)\\Inno Setup 6\\ISCC.exe" installer\\setup.iss'
            }
        }
    }

    post {
        success {
            archiveArtifacts artifacts: 'installer/output/EstoqueApp.exe', fingerprint: true
        }
    }
}