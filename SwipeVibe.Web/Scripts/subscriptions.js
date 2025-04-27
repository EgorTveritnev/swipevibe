document.addEventListener('DOMContentLoaded', function() {
<<<<<<< HEAD:js/subscriptions.js
    // Инициализация страницы подписок
=======
>>>>>>> vadim:SwipeVibe.Web/Scripts/subscriptions.js
    console.log('Страница подписок загружена');
    
    if (window.showToastNotification) {
        showToastNotification('Страница подписок загружена', 'success');
    }
    
    initViewModes();
    
    initStories();
    
    initVideoItems();
    
    initChannelActions();
    
    initFeedActions();
    
    initLoadMore();
});


function initViewModes() {
    const viewModeBtns = document.querySelectorAll('.view-mode-btn');
    const subscriptionsGrid = document.querySelector('.subscriptions-grid');
    const subscriptionsFeed = document.querySelector('.subscriptions-feed');
    
    viewModeBtns.forEach(btn => {
        btn.addEventListener('click', function() {
            viewModeBtns.forEach(b => b.classList.remove('active'));
            
            this.classList.add('active');
            
            const mode = this.getAttribute('data-mode');
            
            if (mode === 'grid') {
                subscriptionsGrid.classList.add('active-view');
                subscriptionsFeed.classList.remove('active-view');
                
                if (window.showToastNotification) {
                    showToastNotification('Режим сетки', 'info');
                }
            } else if (mode === 'feed') {
                subscriptionsGrid.classList.remove('active-view');
                subscriptionsFeed.classList.add('active-view');
                
                if (window.showToastNotification) {
                    showToastNotification('Режим ленты', 'info');
                }
            }
            
            this.classList.add('animate-press');
            setTimeout(() => {
                this.classList.remove('animate-press');
            }, 200);
        });
    });
}


function initStories() {
    const storyItems = document.querySelectorAll('.story-item');
    
    storyItems.forEach(item => {
        item.addEventListener('click', function() {
            this.classList.add('animate-pulse');
            setTimeout(() => {
                this.classList.remove('animate-pulse');
            }, 500);
            
            const username = this.querySelector('.story-username').textContent;
            if (window.showToastNotification) {
                showToastNotification(`История от ${username}`, 'info');
            }
            
            if (this.classList.contains('has-new')) {
                setTimeout(() => {
                    this.classList.remove('has-new');
                }, 200);
            }
            
            simulateStoryView(this);
        });
    });
}


function simulateStoryView(storyItem) {
    const storyModal = document.createElement('div');
    storyModal.className = 'story-modal';
    storyModal.style.position = 'fixed';
    storyModal.style.top = '0';
    storyModal.style.left = '0';
    storyModal.style.width = '100%';
    storyModal.style.height = '100%';
    storyModal.style.backgroundColor = 'rgba(0, 0, 0, 0.9)';
    storyModal.style.zIndex = '9999';
    storyModal.style.display = 'flex';
    storyModal.style.justifyContent = 'center';
    storyModal.style.alignItems = 'center';
    storyModal.style.opacity = '0';
    storyModal.style.transition = 'opacity 0.3s ease';
    
    const avatar = storyItem.querySelector('.story-avatar img').src;
    const username = storyItem.querySelector('.story-username').textContent;
    
    const storyContent = document.createElement('div');
    storyContent.className = 'story-content';
    storyContent.style.width = '100%';
    storyContent.style.maxWidth = '400px';
    storyContent.style.height = '70vh';
    storyContent.style.backgroundColor = 'var(--secondary-color, #151515)';
    storyContent.style.borderRadius = '12px';
    storyContent.style.boxShadow = '0 0 30px rgba(0, 0, 0, 0.7)';
    storyContent.style.position = 'relative';
    storyContent.style.overflow = 'hidden';
    
    const storyHeader = document.createElement('div');
    storyHeader.className = 'story-header';
    storyHeader.style.display = 'flex';
    storyHeader.style.alignItems = 'center';
    storyHeader.style.padding = '15px';
    storyHeader.style.borderBottom = '1px solid rgba(255, 255, 255, 0.1)';
    
    storyHeader.innerHTML = `
        <div style="width: 40px; height: 40px; border-radius: 50%; overflow: hidden; margin-right: 10px; border: 2px solid var(--accent-color-1, #6a67ce);">
            <img src="${avatar}" alt="${username}" style="width: 100%; height: 100%; object-fit: cover;">
        </div>
        <div>
            <div style="color: #fff; font-weight: 600;">${username}</div>
            <div style="color: rgba(255, 255, 255, 0.6); font-size: 12px;">Сейчас</div>
        </div>
        <div style="margin-left: auto; color: #fff; cursor: pointer;" class="close-story">
            <i class="fas fa-times"></i>
        </div>
    `;
    
    const storyMedia = document.createElement('div');
    storyMedia.className = 'story-media';
    storyMedia.style.width = '100%';
    storyMedia.style.height = 'calc(100% - 70px)';
    storyMedia.style.backgroundImage = `url(${storyItem.classList.contains('has-new') ? 'assets/images/video-thumb-1.jpg' : 'assets/images/video-thumb-2.jpg'})`;
    storyMedia.style.backgroundSize = 'cover';
    storyMedia.style.backgroundPosition = 'center';
    
    const progressBar = document.createElement('div');
    progressBar.className = 'progress-bar';
    progressBar.style.height = '3px';
    progressBar.style.width = '0%';
    progressBar.style.backgroundColor = 'var(--accent-color-1, #6a67ce)';
    progressBar.style.position = 'absolute';
    progressBar.style.top = '0';
    progressBar.style.left = '0';
    progressBar.style.transition = 'width 5s linear';
    
    storyContent.appendChild(progressBar);
    storyContent.appendChild(storyHeader);
    storyContent.appendChild(storyMedia);
    storyModal.appendChild(storyContent);
    document.body.appendChild(storyModal);
    
    setTimeout(() => {
        storyModal.style.opacity = '1';
        progressBar.style.width = '100%';
    }, 50);
    
    const closeBtn = storyModal.querySelector('.close-story');
    closeBtn.addEventListener('click', () => {
        storyModal.style.opacity = '0';
        setTimeout(() => {
            document.body.removeChild(storyModal);
        }, 300);
    });
    
    setTimeout(() => {
        storyModal.style.opacity = '0';
        setTimeout(() => {
            document.body.removeChild(storyModal);
        }, 300);
    }, 5000);
}


function initVideoItems() {
    const videoItems = document.querySelectorAll('.video-item');
    
    videoItems.forEach(item => {
        item.addEventListener('click', function(e) {
            if (e.target.closest('.play-btn')) {
                e.preventDefault();
                
                const videoTitle = this.querySelector('.video-title').textContent;
                if (window.showToastNotification) {
                    showToastNotification(`Воспроизведение: ${videoTitle}`, 'info');
                }
                
                simulateVideoPlayback(this);
            } else {
                const videoTitle = this.querySelector('.video-title').textContent;
                if (window.showToastNotification) {
                    showToastNotification(`Открытие видео: ${videoTitle}`, 'info');
                }
            }
        });
        
        item.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-5px)';
            this.style.boxShadow = '-8px -8px 16px rgba(20, 20, 20, 0.6), 8px 8px 16px rgba(0, 0, 0, 0.4), 0 0 15px rgba(106, 103, 206, 0.3)';
            this.style.borderColor = 'rgba(106, 103, 206, 0.3)';
            
            const videoTitle = this.querySelector('.video-title');
            if (videoTitle) {
                videoTitle.style.color = '#ffffff';
                videoTitle.style.textShadow = '0 0 5px rgba(106, 103, 206, 0.2)';
            }
        });
        
        item.addEventListener('mouseleave', function() {
            this.style.transform = '';
            this.style.boxShadow = '';
            this.style.borderColor = '';
            
            const videoTitle = this.querySelector('.video-title');
            if (videoTitle) {
                videoTitle.style.color = '';
                videoTitle.style.textShadow = '';
            }
        });
    });
}


function initChannelActions() {
    const channelActions = document.querySelectorAll('.channel-action');
    
    channelActions.forEach(action => {
        action.addEventListener('click', function(e) {
            e.preventDefault();
            
            this.classList.add('animate-press');
            setTimeout(() => {
                this.classList.remove('animate-press');
            }, 200);
            
            this.classList.toggle('subscribed');
            
            const channelName = this.closest('.channel-header').querySelector('.channel-name').textContent;
            
            if (this.classList.contains('subscribed')) {
                this.innerHTML = '<i class="fas fa-check"></i> Подписан';
                if (window.showToastNotification) {
                    showToastNotification(`Вы подписаны на канал ${channelName}`, 'success');
                }
            } else {
                this.innerHTML = '<i class="fas fa-plus"></i> Подписаться';
                if (window.showToastNotification) {
                    showToastNotification(`Вы отписались от канала ${channelName}`, 'info');
                }
            }
        });
    });
}


function initFeedActions() {
    const feedActionBtns = document.querySelectorAll('.feed-action-btn');
    
    feedActionBtns.forEach(btn => {
        btn.addEventListener('click', function() {
            this.classList.add('animate-press');
            setTimeout(() => {
                this.classList.remove('animate-press');
            }, 200);
            
            let actionType = 'действие';
            if (this.querySelector('i').classList.contains('fa-heart')) {
                actionType = 'лайк';
                
                this.classList.toggle('active');
                
                if (this.classList.contains('active')) {
                    this.style.color = '#ff6b6b';
                    
                    const count = this.querySelector('span');
                    if (count) {
                        const currentCount = parseInt(count.textContent.replace(/[^0-9]/g, ''));
                        count.textContent = (currentCount + 1) + 'K';
                    }
                } else {
                    this.style.color = '';
                    
                    const count = this.querySelector('span');
                    if (count) {
                        const currentCount = parseInt(count.textContent.replace(/[^0-9]/g, ''));
                        count.textContent = (currentCount - 1) + 'K';
                    }
                }
            } else if (this.querySelector('i').classList.contains('fa-comment')) {
                actionType = 'комментарий';
            } else if (this.querySelector('i').classList.contains('fa-share')) {
                actionType = 'репост';
            } else if (this.querySelector('i').classList.contains('fa-bookmark')) {
                actionType = 'закладка';
                
                this.classList.toggle('active');
                
                if (this.classList.contains('active')) {
                    this.style.color = '#1dd1a1';
                } else {
                    this.style.color = '';
                }
            }
            
            const authorName = this.closest('.feed-item').querySelector('.feed-author-name').textContent;
            
            if (window.showToastNotification) {
                if (actionType === 'лайк' && this.classList.contains('active')) {
                    showToastNotification(`Вы поставили лайк на публикацию от ${authorName}`, 'success');
                } else if (actionType === 'лайк') {
                    showToastNotification(`Вы убрали лайк с публикации от ${authorName}`, 'info');
                } else if (actionType === 'закладка' && this.classList.contains('active')) {
                    showToastNotification(`Публикация от ${authorName} добавлена в закладки`, 'success');
                } else if (actionType === 'закладка') {
                    showToastNotification(`Публикация от ${authorName} удалена из закладок`, 'info');
                } else {
                    showToastNotification(`${actionType} на публикацию от ${authorName}`, 'info');
                }
            }
        });
    });
    
    const feedMoreBtns = document.querySelectorAll('.feed-more-btn');
    feedMoreBtns.forEach(btn => {
        btn.addEventListener('click', function() {
            this.classList.add('animate-press');
            setTimeout(() => {
                this.classList.remove('animate-press');
            }, 200);
            
            if (window.showToastNotification) {
                showToastNotification('Дополнительные опции', 'info');
            }
            
            simulateMoreOptions(this);
        });
    });
}


function initLoadMore() {
    const loadMoreBtn = document.querySelector('.load-more-btn');
    
    if (loadMoreBtn) {
        loadMoreBtn.addEventListener('click', function() {
            if (window.showToastNotification) {
                showToastNotification('Загрузка дополнительного контента...', 'info');
            }
            
<<<<<<< HEAD:js/subscriptions.js
            // Изменяем текст кнопки и добавляем класс загрузки
            const originalText = this.innerHTML;
            this.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Загрузка...';
            this.disabled = true;
            
            // Добавляем эффект свечения для загрузки
            this.style.boxShadow = '0 0 15px rgba(106, 103, 206, 0.4)';
            this.style.background = 'rgba(106, 103, 206, 0.3)';
            
            // Имитируем загрузку новых видео
            setTimeout(() => {
                // Восстанавливаем оригинальный текст кнопки
=======
            const originalText = this.innerHTML;
            this.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Загрузка...';
            this.disabled = true;
            this.style.boxShadow = '0 0 15px rgba(106, 103, 206, 0.4)';
            this.style.background = 'rgba(106, 103, 206, 0.3)';
            
            setTimeout(() => {
>>>>>>> vadim:SwipeVibe.Web/Scripts/subscriptions.js
                this.innerHTML = originalText;
                this.disabled = false;
                this.style.boxShadow = '';
                this.style.background = '';
<<<<<<< HEAD:js/subscriptions.js
                
                // В реальном приложении здесь был бы API запрос
=======
>>>>>>> vadim:SwipeVibe.Web/Scripts/subscriptions.js
                loadMoreContent();
                
                if (window.showToastNotification) {
                    showToastNotification('Загружен новый контент', 'success');
                }
            }, 1500);
        });
    }
}


function simulateVideoPlayback(videoCard) {
<<<<<<< HEAD:js/subscriptions.js
    // В реальном приложении здесь был бы код для воспроизведения видео
    
    // Эффект подсветки карточки
    videoCard.style.boxShadow = '-8px -8px 16px rgba(20, 20, 20, 0.6), 8px 8px 16px rgba(0, 0, 0, 0.4), 0 0 20px rgba(106, 103, 206, 0.5)';
    videoCard.style.borderColor = 'rgba(106, 103, 206, 0.3)';
    
    // Эффект для кнопки воспроизведения
=======
    videoCard.style.boxShadow = '-8px -8px 16px rgba(20, 20, 20, 0.6), 8px 8px 16px rgba(0, 0, 0, 0.4), 0 0 20px rgba(106, 103, 206, 0.5)';
    videoCard.style.borderColor = 'rgba(106, 103, 206, 0.3)';
>>>>>>> vadim:SwipeVibe.Web/Scripts/subscriptions.js
    const playBtn = videoCard.querySelector('.play-btn');
    if (playBtn) {
        playBtn.style.background = 'var(--accent-color-1, #6a67ce)';
        playBtn.style.boxShadow = '0 0 25px rgba(106, 103, 206, 0.7)';
    }
    
    setTimeout(() => {
        videoCard.style.boxShadow = '';
        videoCard.style.borderColor = '';
        
        if (playBtn) {
            playBtn.style.background = '';
            playBtn.style.boxShadow = '';
        }
    }, 500);
}


function simulateMoreOptions(btn) {
<<<<<<< HEAD:js/subscriptions.js
    // Проверяем, есть ли уже меню
=======
>>>>>>> vadim:SwipeVibe.Web/Scripts/subscriptions.js
    let optionsMenu = document.querySelector('.feed-options-menu');
    if (optionsMenu) {
        optionsMenu.remove();
        return;
    }
    
<<<<<<< HEAD:js/subscriptions.js
    // Создаем меню опций
=======
>>>>>>> vadim:SwipeVibe.Web/Scripts/subscriptions.js
    optionsMenu = document.createElement('div');
    optionsMenu.className = 'feed-options-menu';
    optionsMenu.style.position = 'absolute';
    optionsMenu.style.top = btn.offsetTop + btn.offsetHeight + 5 + 'px';
    optionsMenu.style.right = '20px';
    optionsMenu.style.width = '200px';
    optionsMenu.style.backgroundColor = 'var(--secondary-color, #151515)';
    optionsMenu.style.borderRadius = '8px';
    optionsMenu.style.boxShadow = '0 0 20px rgba(0, 0, 0, 0.5), 0 0 15px rgba(106, 103, 206, 0.1)';
    optionsMenu.style.padding = '8px 0';
    optionsMenu.style.zIndex = '100';
    optionsMenu.style.border = '1px solid rgba(255, 255, 255, 0.05)';
    
<<<<<<< HEAD:js/subscriptions.js
    // Опции
=======
>>>>>>> vadim:SwipeVibe.Web/Scripts/subscriptions.js
    const options = [
        { icon: 'fas fa-ban', text: 'Скрыть публикацию' },
        { icon: 'fas fa-flag', text: 'Пожаловаться' },
        { icon: 'fas fa-copy', text: 'Копировать ссылку' },
        { icon: 'fas fa-bell-slash', text: 'Отключить уведомления' }
    ];
    
<<<<<<< HEAD:js/subscriptions.js
    // Добавляем опции в меню
=======
>>>>>>> vadim:SwipeVibe.Web/Scripts/subscriptions.js
    options.forEach(option => {
        const optionItem = document.createElement('div');
        optionItem.className = 'menu-option';
        optionItem.style.padding = '10px 15px';
        optionItem.style.color = 'rgba(255, 255, 255, 0.8)';
        optionItem.style.fontSize = '14px';
        optionItem.style.cursor = 'pointer';
        optionItem.style.transition = 'all 0.2s ease';
        optionItem.style.display = 'flex';
        optionItem.style.alignItems = 'center';
        optionItem.style.gap = '10px';
        
        optionItem.innerHTML = `
            <i class="${option.icon}" style="width: 20px; text-align: center;"></i>
            <span>${option.text}</span>
        `;
        
        optionItem.addEventListener('mouseenter', function() {
            this.style.backgroundColor = 'rgba(255, 255, 255, 0.05)';
            this.style.color = '#fff';
        });
        
        optionItem.addEventListener('mouseleave', function() {
            this.style.backgroundColor = '';
            this.style.color = 'rgba(255, 255, 255, 0.8)';
        });
        
        optionItem.addEventListener('click', function() {
            if (window.showToastNotification) {
                showToastNotification(`Выбрано: ${option.text}`, 'info');
            }
            optionsMenu.remove();
        });
        
        optionsMenu.appendChild(optionItem);
    });
    
<<<<<<< HEAD:js/subscriptions.js
    // Добавляем меню в DOM
    btn.closest('.feed-item').style.position = 'relative';
    btn.closest('.feed-item').appendChild(optionsMenu);
    
    // Добавляем обработчик для закрытия меню при клике вне его
=======
    btn.closest('.feed-item').style.position = 'relative';
    btn.closest('.feed-item').appendChild(optionsMenu);
    
>>>>>>> vadim:SwipeVibe.Web/Scripts/subscriptions.js
    document.addEventListener('click', function closeMenu(e) {
        if (!optionsMenu.contains(e.target) && e.target !== btn) {
            optionsMenu.remove();
            document.removeEventListener('click', closeMenu);
        }
    });
}


function loadMoreContent() {
    
    const channelsContainer = document.querySelector('.subscriptions-grid');
    if (channelsContainer) {
        
        const existingChannels = document.querySelectorAll('.channel-section');
        if (existingChannels.length > 0) {
            const newChannel = existingChannels[0].cloneNode(true);
            
            const channelName = newChannel.querySelector('.channel-name');
            channelName.textContent = "NewChannel";
            
            const videoTitles = newChannel.querySelectorAll('.video-title');
            videoTitles.forEach((title, index) => {
                title.textContent = `Новое видео ${index + 1}: ${title.textContent}`;
            });
            
            const firstVideo = newChannel.querySelector('.video-thumbnail');
            if (firstVideo && !firstVideo.classList.contains('new')) {
                firstVideo.classList.add('new');
                
                if (firstVideo.classList.contains('trending')) {
                    firstVideo.classList.remove('trending');
                    const trendingBadge = firstVideo.querySelector('.trending-badge');
                    if (trendingBadge) {
                        trendingBadge.remove();
                    }
                }
                
                if (!firstVideo.querySelector('.new-badge')) {
                    const newBadge = document.createElement('div');
                    newBadge.className = 'new-badge';
                    newBadge.innerHTML = '<i class="fas fa-certificate"></i> Новое';
                    firstVideo.appendChild(newBadge);
                }
            }
            
            channelsContainer.appendChild(newChannel);
            
            initVideoItems();
            initChannelActions();
        }
    }
    
    const feedContainer = document.querySelector('.subscriptions-feed');
    if (feedContainer) {
        
        const existingFeedItems = document.querySelectorAll('.feed-item');
        if (existingFeedItems.length > 0) {
            const newFeedItem = existingFeedItems[existingFeedItems.length - 1].cloneNode(true);
            
            const feedAuthorName = newFeedItem.querySelector('.feed-author-name');
            if (feedAuthorName) {
                feedAuthorName.textContent = "NewChannel";
            }
            
            const feedTime = newFeedItem.querySelector('.feed-time');
            if (feedTime) {
                feedTime.textContent = "Только что";
            }
            
            const feedDescription = newFeedItem.querySelector('.feed-description');
            if (feedDescription) {
                feedDescription.textContent = "Новая публикация с уникальным контентом, который вы еще не видели. Проверьте это видео прямо сейчас!";
            }
            
            const thumbnail = newFeedItem.querySelector('.feed-thumbnail');
            if (thumbnail && !thumbnail.classList.contains('new')) {
                thumbnail.classList.add('new');
                
                if (!thumbnail.querySelector('.new-badge')) {
                    const newBadge = document.createElement('div');
                    newBadge.className = 'new-badge';
                    newBadge.innerHTML = '<i class="fas fa-certificate"></i> Новое';
                    thumbnail.appendChild(newBadge);
                }
            }
            
            const actionCounts = newFeedItem.querySelectorAll('.feed-action-btn span');
            actionCounts.forEach(count => {
                const randomNum = Math.floor(Math.random() * 100);
                count.textContent = randomNum + 'K';
            });
            
            feedContainer.appendChild(newFeedItem);
            
<<<<<<< HEAD:js/subscriptions.js
            // Обновляем обработчики событий
=======
>>>>>>> vadim:SwipeVibe.Web/Scripts/subscriptions.js
            initFeedActions();
        }
    }
} 