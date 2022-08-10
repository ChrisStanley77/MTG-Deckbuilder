package com.neumont.edu.sen300.emailservice.models;

import javax.mail.*;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeBodyPart;
import javax.mail.internet.MimeMessage;
import javax.mail.internet.MimeMultipart;
import java.util.Properties;

public class SendMail {

    final static String senderEmail = System.getenv("EMAIL");
    final static String senderPassword = System.getenv("EMAIL_PASSWORD");
    final String emailSMTPServer = "smtp.gmail.com";
    final String emailServerPort = "465";
    String receiverEmail = null;
    static String emailSubject;
    static String emailBody;

    /**
     * This is a class in which you can send emails through java.
     * @param receiverEmail String of a person's email you want to send to
     * @param subject String of what the email subject will be
     * @param body String containing the message being sent to the user
     */
    public SendMail(String receiverEmail, String subject, String body) {
        this.receiverEmail = receiverEmail;
        this.emailSubject = subject;
        this.emailBody = body;

        // Setting up the properties for sending email
        Properties props = new Properties();
        props.put("mail.smtp.user", senderEmail);
        props.put("mail.smtp.host", emailSMTPServer);
        props.put("mail.smtp.port", emailServerPort);
        props.put("mail.smtp.starttls.enable", "true");
        props.put("mail.smtp.starttls.required", "true");
        props.put("mail.smtp.ssl.protocols", "TLSv1.2");
        props.put("mail.smtp.auth", "true");
        props.put("mail.smtp.socketFactory.port", emailServerPort);
        props.put("mail.smtp.socketFactory.class", "javax.net.ssl.SSLSocketFactory");
        props.put("mail.smtp.socketFactory.fallback", "false");

        try {
            Authenticator auth = new SMTPAuthenticator();
            Session session = Session.getInstance(props, auth);
            MimeMessage msg = new MimeMessage(session);
            msg.setContent(emailBody, "text/html");
            msg.setSubject(emailSubject);
            msg.setFrom(new InternetAddress(senderEmail, "E-Commerce"));
            msg.addRecipient(Message.RecipientType.TO, new InternetAddress(receiverEmail));
            Transport.send(msg);
            System.out.println("Email Sent Successfully");
        } catch(Exception e) {
            e.printStackTrace();
        }
    }
}
