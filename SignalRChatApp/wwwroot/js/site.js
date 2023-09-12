
const initializeSignalRConnection = () => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub")
        .build();

    
    connection.on("ReceiveMessage", ({ userName, message }) => {
        const messageList = document.getElementById("messages-list");

        const newMessage = `<li>${userName}: ${message}</li>`;

        messageList.innerHTML += newMessage;
    });

    connection.start().catch(err => console.error(err.toSring()));
    return connection;
}

const connection = initializeSignalRConnection();

const send = () => {
    var userName = document.getElementById("userName").value;
    var message = document.getElementById("message-input").value;

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