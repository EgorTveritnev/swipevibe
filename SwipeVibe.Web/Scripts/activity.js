<<<<<<< HEAD:js/activity.js
// JavaScript для страницы активности

=======
>>>>>>> vadim:SwipeVibe.Web/Scripts/activity.js
document.addEventListener('DOMContentLoaded', function() {
    const tabButtons = document.querySelectorAll('.tab-btn');
    const tabContents = document.querySelectorAll('.tab-content');
    
    tabButtons.forEach(button => {
        button.addEventListener('click', () => {
            tabButtons.forEach(btn => btn.classList.remove('active'));
            tabContents.forEach(content => content.classList.remove('active'));
            
            button.classList.add('active');
            
            const tabName = button.getAttribute('data-tab');
            document.getElementById(`${tabName}-content`).classList.add('active');
        });
    });
    
    const activityItems = document.querySelectorAll('.activity-item.new');
    activityItems.forEach(item => {
        item.addEventListener('click', () => {
            item.classList.remove('new');
            updateNotificationCount();
        });
    });
    
    function updateNotificationCount() {
        const unreadCount = document.querySelectorAll('.activity-item.new').length;
        const notificationBadges = document.querySelectorAll('.notification-badge');
        const tabBadge = document.querySelector('.tab-btn[data-tab="notifications"] .badge');
        
        if (unreadCount === 0) {
            notificationBadges.forEach(badge => {
                badge.style.display = 'none';
            });
            
            if (tabBadge) {
                tabBadge.style.display = 'none';
            }
        } else {
            notificationBadges.forEach(badge => {
                badge.style.display = 'flex';
                badge.textContent = unreadCount;
            });
            
            if (tabBadge) {
                tabBadge.style.display = 'inline-block';
                tabBadge.textContent = unreadCount;
            }
        }
    }
<<<<<<< HEAD:js/activity.js
    
    // Функционал чата
=======
>>>>>>> vadim:SwipeVibe.Web/Scripts/activity.js
    const chatItems = document.querySelectorAll('.chat-item');
    chatItems.forEach(chat => {
        chat.addEventListener('click', () => {
            chatItems.forEach(c => c.classList.remove('active'));
            
            chat.classList.add('active');
            
            const unreadCount = chat.querySelector('.unread-count');
            if (unreadCount) {
                unreadCount.remove();
            }
        });
    });
    
    const messageForm = document.querySelector('.chat-input');
    const messageInput = messageForm ? messageForm.querySelector('input') : null;
    const sendButton = messageForm ? messageForm.querySelector('.send-btn') : null;
    
    if (messageForm && messageInput && sendButton) {
        function sendMessage() {
            const messageText = messageInput.value.trim();
            if (messageText === '') return;
            
            const chatMessages = document.querySelector('.chat-messages');
            
            const newMessage = document.createElement('div');
            newMessage.className = 'message sent';
            
            const now = new Date();
            const hours = now.getHours().toString().padStart(2, '0');
            const minutes = now.getMinutes().toString().padStart(2, '0');
            const timeString = `${hours}:${minutes}`;
            
            newMessage.innerHTML = `
                <div class="message-content">
                    <div class="message-bubble">
                        ${messageText}
                    </div>
                    <div class="message-time">${timeString}</div>
                </div>
            `;
            
            chatMessages.appendChild(newMessage);
            
            chatMessages.scrollTop = chatMessages.scrollHeight;
            
            messageInput.value = '';
            
            messageInput.focus();
<<<<<<< HEAD:js/activity.js
            
            // Симуляция ответа через 1-3 секунды
=======
>>>>>>> vadim:SwipeVibe.Web/Scripts/activity.js
            setTimeout(simulateReply, Math.random() * 2000 + 1000);
        }
        
        function simulateReply() {
            const chatMessages = document.querySelector('.chat-messages');
            
            const newMessage = document.createElement('div');
            newMessage.className = 'message received';
            
            const now = new Date();
            const hours = now.getHours().toString().padStart(2, '0');
            const minutes = now.getMinutes().toString().padStart(2, '0');
            const timeString = `${hours}:${minutes}`;
            
            const responses = [
                'Да, конечно!',
                'Интересная идея.',
                'Давай обсудим это подробнее.',
                'Хорошо, я подумаю над этим.',
                'Спасибо за информацию!',
                'Это звучит здорово!',
                'Согласен с тобой.'
            ];
            
            const randomResponse = responses[Math.floor(Math.random() * responses.length)];
            
            newMessage.innerHTML = `
                <div class="message-avatar">
                    <img src="assets/images/avatar1.jpg" alt="Аватар">
                </div>
                <div class="message-content">
                    <div class="message-bubble">
                        ${randomResponse}
                    </div>
                    <div class="message-time">${timeString}</div>
                </div>
            `;
            
            chatMessages.appendChild(newMessage);
            
            chatMessages.scrollTop = chatMessages.scrollHeight;
        }
        
        sendButton.addEventListener('click', sendMessage);
        
        messageInput.addEventListener('keypress', (e) => {
            if (e.key === 'Enter') {
                sendMessage();
                e.preventDefault();
            }
        });
    }
    
    const chatMessages = document.querySelector('.chat-messages');
    if (chatMessages) {
        chatMessages.scrollTop = chatMessages.scrollHeight;
    }
    
    function animateActivityItems() {
        const activityItems = document.querySelectorAll('.activity-item');
        activityItems.forEach((item, index) => {
            item.style.opacity = '0';
            item.style.transform = 'translateY(20px)';
            item.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
            
            setTimeout(() => {
                item.style.opacity = '1';
                item.style.transform = 'translateY(0)';
            }, 100 * index);
        });
    }
    
    animateActivityItems();
}); 