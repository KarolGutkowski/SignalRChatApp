
const appendNewMessageToChatInterface = (userName, message) => {
  const messageInbox = document.getElementById("msg-page");
  const receivedMsg = document.createElement("div");
  receivedMsg.className = "received-msg";
  receivedMsg.innerHTML = `<div class="received-msg-inbox">
                                <p class="single-msg">${message}</p>
                                <span class="time">18:31 PM | July 24</span>
                            </div>`;
  messageInbox.appendChild(receivedMsg);
};

const initializeSignalRConnection = (chatRoomName) => {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

  connection.on("ReceiveMessage", ({ userName, message }) => {
    appendNewMessageToChatInterface(userName, message);
  });

  let timerId;

  connection.on("ReceiveSomeoneWriting", () => {
    const notifyUserWriting = document.getElementById(
      "another-user-writing-notifiaction"
    );
    console.log("someone writing");
    notifyUserWriting.classList.remove("hidden");
    if (timerId) {
      clearTimeout(timerId);
    }
    timerId = setTimeout(() => {
      notifyUserWriting.classList.add("hidden");
    }, 1500);
  });

  connection.on("GetAllMessages", async (allMessages) => {
    for (let msg of allMessages) {
      await appendNewMessageToChatInterface(msg.userName, msg.message);
    }
  });

  connection.start().catch((err) => console.error(err.toSring()));
  return connection;
};

const connection = initializeSignalRConnection();

const send = () => {
  var userName = "placeholder";
  var messageInput = document.getElementById("message-input");
  var message = messageInput.value;
  messageInput.value = "";

  const messagePage = document.getElementById("msg-page");
  var messageMarkup = `<div class="outgoing-chats">
                                <div class="outgoing-msg">
                                  <div class="outgoing-chats-msg">
                                    <p>
                                      ${message}
                                    </p>
                                    <span class="time">18:34 PM | July 24</span>
                                  </div>
                                </div>
                            </div>`;
  messagePage.innerHTML += messageMarkup;

  scrollToBottom(messagePage);

  console.log(userName + " " + message);

  fetch(`/chathub/${userName}/newmessage`, {
    method: "POST",
    body: JSON.stringify({ message }),
    headers: {
      "Content-Type": "application/json",
    },
  });

  connection.invoke("NotifyNewMessage", {
    username: userName,
    message: message,
  });
};

const notifyUserWritingMessage = () => {
  console.log("user writing message");

  connection.invoke("NotifyThatImWriting");
};

function scrollToBottom(element) {
  element.scrollTop = element.scrollHeight;
}
