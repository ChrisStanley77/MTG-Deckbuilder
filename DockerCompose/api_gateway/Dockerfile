# Setting up the Email Service
FROM tomcat:8-jdk8-openjdk

COPY Gateway-1.0.0.jar /usr/local/lib/Gateway.jar

ENTRYPOINT ["java","-jar","/usr/local/lib/Gateway.jar"]

EXPOSE 8888