/* groovylint-disable CompileStatic, DuplicateStringLiteral, LineLength */
pipeline {
    agent any
    environment {
        DOCKER_IMAGE_NAME = 'auth-dev:0.0'
        CONTAINER_NAME = 'auth-dev'
        APP_PORT = '5280'
        GIT_REPO = 'https://github.com/moein-rezaee/Authenticate.git'
        LOCAL_GIT_REPO = '/home/moein/Desktop/Jenkins/Authenticate'
        PROJECT_DIR = './Authenticate'
        ACTIVE_BRANCH = 'develop'
        DEV_MODE = 'ASPNETCORE_ENVIRONMENT=Development'
    }
    stages {
        stage('Show Current Directory') {
            steps {
                sh 'echo "Current Directory:"'
                sh 'pwd'
                sh 'echo "Directory Contents:"'
                sh 'ls -la'
            }
        }
        // stage('Checkout') {
        //     steps {
        //         sh "git clone ${LOCAL_GIT_REPO} || true"
        //         dir("${PROJECT_DIR}") {
        //             script {
        //                 sh 'echo "Current Directory:"'
        //                 sh 'pwd'

        //                 sh "git checkout ${ACTIVE_BRANCH} || true"
        //                 sh 'git fetch --all'
        //                 sh "git pull origin ${ACTIVE_BRANCH}"

        //                 sh 'echo "Directory Contents:"'
        //                 sh 'ls -la'
        //             }
        //         }
        //     }
        // }
        // stage('Remove Old Image') {
        //     steps {
        //         script {
        //             sh "docker rm -f ${CONTAINER_NAME} || true"
        //             sh "docker rmi -f ${DOCKER_IMAGE_NAME} || true"
        //         }
        //     }
        // }
        // stage('Build New Image') {
        //     steps {
        //         dir("${PROJECT_DIR}") {
        //             script {
        //                 sh "docker build -t ${DOCKER_IMAGE_NAME} ."
        //             }
        //         }
        //     }
        // }
        // stage('Run New Container') {
        //     steps {
        //         script {
        //             sh "docker run --name ${CONTAINER_NAME} -e ${DEV_MODE} -p ${APP_PORT}:${APP_PORT} -dit --rm ${DOCKER_IMAGE_NAME}"
        //         }
        //     }
        // }
    }
    post {
        always {
            sh "docker logs ${CONTAINER_NAME} || true"
        }
    }
};
