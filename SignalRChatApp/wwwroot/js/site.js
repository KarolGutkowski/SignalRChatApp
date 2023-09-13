﻿
const initializeSignalRConnection = () => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub")
        .build();

    
    connection.on("ReceiveMessage", ({ userName, message }) => {
        const messageList = document.getElementById("messages-list");

        const newMessage = `<li>${userName}: ${message}</li>`;

        messageList.innerHTML += newMessage;
    });

    connection.on("ReceiveSomeoneWriting", () => {
        console.log("some other user is actively writing");
    });

    connection.start().catch(err => console.error(err.toSring()));
    return connection;
}

const connection = initializeSignalRConnection();

const send = () => {
    var userName = document.getElementById("userName").value;
    var messageInput = document.getElementById("message-input");
    var message = messageInput.value;
    messageInput.value = "";

    console.log(userName + " " + message);

    fetch(`/chathub/${userName}/newmessage`, {
        method: "POST",
        body: JSON.stringify({ message }),
        headers: {
            'Content-Type': 'application/json'
        }
    });

    connection.invoke("NotifyNewMessage", {
        username: userName,
        message: message
    });
}

const notifyUserWritingMessage = () => {
    console.log("user writing message");

    connection.invoke("NotifyThatImWriting");
}