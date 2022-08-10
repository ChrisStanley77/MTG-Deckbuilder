package com.neumont.edu.sen300.emailservice.models;

public class Email {
    private String receiverEmail;
    private String emailSubject;
    private String emailBody;

    public Email() {

    }

    public Email(String receiverEmail, String emailSubject, String emailBody) {

        if(receiverEmail == null) throw new NullPointerException("Receiver Email is null");

        if(emailSubject == null) throw new NullPointerException("emailSubject is null");

        this.receiverEmail = receiverEmail;
        this.emailSubject = emailSubject;
        this.emailBody = emailBody;
    }

    public String getReceiverEmail() {
        return receiverEmail;
    }

    public void setReceiverEmail(String receiverEmail) {
        this.receiverEmail = receiverEmail;
    }

    public String getEmailSubject() {
        return emailSubject;
    }

    public void setEmailSubject(String emailSubject) {
        this.emailSubject = emailSubject;
    }

    public String getEmailBody() {
        return emailBody;
    }

    public void setEmailBody(String emailBody) {
        this.emailBody = emailBody;
    }

    @Override
    public String toString() {
        return "{" +
                "   Receiver: " + receiverEmail +
                "   Subject: " + emailSubject +
                "   Email Body: " + emailBody +
                "}";
    }
}
