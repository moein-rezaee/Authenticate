/* groovylint-disable CompileStatic, DuplicateStringLiteral, LineLength */
pipeline {
    agent any
    environment {
        DOCKER_IMAGE_NAME = 'auth:0.0'
        CONTAINER_NAME = 'auth'
        APP_PORT = '5280'
        GIT_REPO = 'https://github.com/moein-rezaee/Authenticate.git'
        LOCAL_GIT_REPO = '/home/moein/Desktop/Jenkins/Authenticate'
        PROJECT_DIR = './Authenticate'
        ACTIVE_BRANCH = 'main'
        DEV_MODE = 'ASPNETCORE_ENVIRONMENT=Development'
        DEPLOY_ENV = "blue"
        DEPLOY_PORT = "5280"
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
        stage('Checkout') {
            steps {
                sh "git clone ${LOCAL_GIT_REPO} || true"
                dir("${PROJECT_DIR}") {
                    script {
                        sh 'echo "Current Directory:"'
                        sh 'pwd'

                        sh "git checkout ${ACTIVE_BRANCH} || true"
                        sh 'git fetch --all'
                        sh "git pull origin ${ACTIVE_BRANCH}"

                        sh 'echo "Directory Contents:"'
                        sh 'ls -la'
                    }
                }
            }
        }
        stage('Remove Old Image') {
            steps {
                script {
                    if (env.DEPLOY_ENV == "blue") {
                        DEPLOY_ENV = "green"
                        DEPLOY_PORT = "5281"
                    } else {
                        DEPLOY_ENV = "blue"
                        DEPLOY_PORT = "5280"
                    }
                    echo "Active Environment is: ${env.DEPLOY_ENV}"
                    echo "Deploying to ${DEPLOY_ENV} environment..."
                    sh "docker rm -f ${DEPLOY_ENV}-${CONTAINER_NAME} || true"
                    sh "docker rmi -f ${DEPLOY_ENV}-${CONTAINER_NAME} || true"
                }
            }
        }
        stage('Build New Image') {
            steps {
                dir("${PROJECT_DIR}") {
                    script {
                        sh "docker build -t ${DEPLOY_ENV}-${DOCKER_IMAGE_NAME} ."
                    }
                }
            }
        }
        stage('Run New Container') {
            steps {
                script {
                    sh "docker run --name ${DEPLOY_ENV}-${CONTAINER_NAME} -e ${DEV_MODE} -p ${DEPLOY_PORT}:${APP_PORT} -dit --rm ${DEPLOY_ENV}-${DOCKER_IMAGE_NAME}"
                }
            }
        }
        stage('Remove Old Environment') {
            steps {
                script {
                    if (env.DEPLOY_ENV == "blue") {
                        DEPLOY_ENV = "green"
                        DEPLOY_PORT = "5281"
                    
                        echo "Remove Environment: blue"
                        sh "docker rm -f blue-${CONTAINER_NAME} || true"
                        sh "docker rmi -f blue-${CONTAINER_NAME} || true"
                    } else {
                        DEPLOY_ENV = "blue"
                        DEPLOY_PORT = "5280"

                        echo "Remove Environment: green"
                        sh "docker rm -f green-${CONTAINER_NAME} || true"
                        sh "docker rmi -f green-${CONTAINER_NAME} || true"
                    }
                }
            }
        }
    }
    post {
        always {
            sh "docker logs ${DEPLOY_ENV}-${CONTAINER_NAME} || true"
        }
    }
};
