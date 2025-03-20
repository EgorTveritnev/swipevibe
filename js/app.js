// Глобальные функции для приложения SwipeVibe

// При загрузке DOM
document.addEventListener('DOMContentLoaded', function() {
    console.log('SwipeVibe инициализация в тёмной теме...');
    
    initializeApp();
    
    initVideos();
    initNavigation();
    initDesktopControls();
    initSideNav();
    initAnimations();
    initSwipeGestures();
    
    addGlowEffects();
    
    setupVideoOrder();
});

function initializeApp() {
    console.log('SwipeVibe App initialized');
    
    window.showToastNotification = function(message, type = 'info') {
        const toastContainer = document.querySelector('.toast-container');
        
        if (!toastContainer) {
            console.error('Toast container not found');
            return;
        }
        
        const toast = document.createElement('div');
        toast.className = `toast toast-${type}`;
        
        toast.style.background = 'rgba(14, 14, 18, 0.95)';
        toast.style.boxShadow = '0 0 15px rgba(0, 0, 0, 0.5), 0 0 10px rgba(106, 103, 206, 0.3)';
        toast.style.backdropFilter = 'blur(10px)';
        toast.style.border = '1px solid rgba(106, 103, 206, 0.2)';
        
        let iconClass = 'fa-info-circle';
        
        switch (type) {
            case 'success':
                iconClass = 'fa-check-circle';
                toast.style.borderColor = 'rgba(46, 213, 115, 0.5)';
                toast.style.boxShadow += ', 0 0 15px rgba(46, 213, 115, 0.3)';
                break;
            case 'error':
                iconClass = 'fa-exclamation-circle';
                toast.style.borderColor = 'rgba(255, 71, 87, 0.5)';
                toast.style.boxShadow += ', 0 0 15px rgba(255, 71, 87, 0.3)';
                break;
            case 'warning':
                iconClass = 'fa-exclamation-triangle';
                toast.style.borderColor = 'rgba(255, 165, 2, 0.5)';
                toast.style.boxShadow += ', 0 0 15px rgba(255, 165, 2, 0.3)';
                break;
            case 'info':
            default:
                toast.style.borderColor = 'rgba(106, 103, 206, 0.3)';
                toast.style.boxShadow += ', 0 0 15px rgba(106, 103, 206, 0.3)';
                break;
        }
        
        toast.innerHTML = `
            <div class="toast-with-icon">
                <div class="toast-icon">
                    <i class="fas ${iconClass}"></i>
                </div>
                <div class="toast-content">${message}</div>
            </div>
        `;
        
        toastContainer.appendChild(toast);
        
        setTimeout(() => toast.classList.add('show'), 10);
        
        setTimeout(() => {
            toast.classList.remove('show');
            toast.classList.add('hide');
            
            setTimeout(() => {
                toast.remove();
            }, 300);
        }, 3000);
    };
}

// Инициализация видео
function initVideos() {
    const videos = document.querySelectorAll('.video-card video');
    const videoCards = document.querySelectorAll('.video-card');
    
    if (videoCards.length > 0) {
        videoCards[0].classList.add('active');
        const activeVideo = videoCards[0].querySelector('video');
        
        activeVideo.play().catch(error => {
            console.log('Автовоспроизведение заблокировано. Пользователю нужно взаимодействовать со страницей:', error);
        });
        
        activeVideo.loop = true;
        activeVideo.muted = true;
    }
    
    videos.forEach(video => {
        video.addEventListener('click', function() {
            if (video.paused) {
                video.play();
            } else {
                video.pause();
            }
        });
    });
    
    if (!document.querySelector('.swipe-indicator')) {
        const swipeIndicator = document.createElement('div');
        swipeIndicator.className = 'swipe-indicator';
        swipeIndicator.innerHTML = '<i class="fas fa-chevron-up"></i> Свайп <i class="fas fa-chevron-down"></i>';
        document.querySelector('.desktop-video-feed').appendChild(swipeIndicator);
    }
}

function setupVideoOrder() {
    const allCards = document.querySelectorAll('.video-card');
    const totalCards = allCards.length;
    
    if (totalCards < 2) return;
    
    const activeCard = document.querySelector('.video-card.active');
    if (!activeCard) return;
    
    const currentIndex = Array.from(allCards).indexOf(activeCard);
    
    const prevIndex = (currentIndex - 1 + totalCards) % totalCards;
    const nextIndex = (currentIndex + 1) % totalCards;
    
    allCards.forEach(card => {
        card.classList.remove('prev', 'next');
    });
    
    allCards[prevIndex].classList.add('prev');
    allCards[nextIndex].classList.add('next');
    
    const activeVideo = activeCard.querySelector('video');
    if (activeVideo) {
        activeVideo.currentTime = 0;
        activeVideo.play().catch(error => {
            console.log('Автовоспроизведение заблокировано:', error);
        });
    }
    
    allCards.forEach((card, index) => {
        if (index !== currentIndex) {
            const video = card.querySelector('video');
            if (video) {
                video.pause();
            }
        }
    });
}

// Инициализация навигации
function initNavigation() {
    const nextBtn = document.querySelector('.next-btn');
    const prevBtn = document.querySelector('.prev-btn');
    
    if (nextBtn) {
        nextBtn.addEventListener('click', showNextVideo);
    }
    
    if (prevBtn) {
        prevBtn.addEventListener('click', showPrevVideo);
    }
    
    document.addEventListener('keydown', function(e) {
        if (e.key === 'ArrowDown') {
            showNextVideo();
        } else if (e.key === 'ArrowUp') {
            showPrevVideo();
        }
    });

    const sideNavLinks = document.querySelectorAll('.side-nav-item');
    sideNavLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            const href = this.getAttribute('href');
            if (href && href !== '#') {
                window.location.href = href;
            }
        });
    });
}

function showSwipeIndicator() {
    const indicator = document.querySelector('.swipe-indicator');
    if (indicator) {
        indicator.classList.add('visible');
        setTimeout(() => {
            indicator.classList.remove('visible');
        }, 1500);
    }
}

function showNextVideo() {
    const activeCard = document.querySelector('.video-card.active');
    const nextCard = document.querySelector('.video-card.next');
    const allCards = document.querySelectorAll('.video-card');
    const totalCards = allCards.length;
    
    if (!activeCard || !nextCard || totalCards < 2) return;
    
    allCards.forEach(card => {
        card.classList.remove('sliding-prev', 'sliding-next', 'sliding-out-top', 'sliding-out-bottom');
    });
    
    activeCard.classList.add('sliding-out-top');
    
    nextCard.classList.add('sliding-next');
    
    showSwipeIndicator();
    
    setTimeout(() => {
        activeCard.classList.remove('active', 'sliding-out-top');
        nextCard.classList.remove('next', 'sliding-next');
        nextCard.classList.add('active');
        
        setupVideoOrder();
        
        showToastNotification('Следующее видео');
    }, 500);
}

function showPrevVideo() {
    const activeCard = document.querySelector('.video-card.active');
    const prevCard = document.querySelector('.video-card.prev');
    const allCards = document.querySelectorAll('.video-card');
    const totalCards = allCards.length;
    
    if (!activeCard || !prevCard || totalCards < 2) return;
    
    allCards.forEach(card => {
        card.classList.remove('sliding-prev', 'sliding-next', 'sliding-out-top', 'sliding-out-bottom');
    });
    
    activeCard.classList.add('sliding-out-bottom');
    
    prevCard.classList.add('sliding-prev');
    
    showSwipeIndicator();
    
    setTimeout(() => {
        activeCard.classList.remove('active', 'sliding-out-bottom');
        prevCard.classList.remove('prev', 'sliding-prev');
        prevCard.classList.add('active');
        
        setupVideoOrder();
        
        showToastNotification('Предыдущее видео');
    }, 500);
}

function initSwipeGestures() {
    const videoFeed = document.querySelector('.desktop-video-feed');
    
    if (!videoFeed) return;
    
    let startY = 0;
    let endY = 0;
    const minDistance = 50;
    
    videoFeed.addEventListener('touchstart', function(e) {
        startY = e.touches[0].clientY;
    }, { passive: true });
    
    videoFeed.addEventListener('touchend', function(e) {
        endY = e.changedTouches[0].clientY;
        
        const distance = startY - endY;
        
        if (Math.abs(distance) >= minDistance) {
            if (distance > 0) {
                showNextVideo();
            } else {
                showPrevVideo();
            }
        }
    }, { passive: true });
    
    videoFeed.addEventListener('wheel', function(e) {
        e.preventDefault();
        
        if (e.deltaY > 0) {
            showNextVideo();
        } else {
            showPrevVideo();
        }
    }, { passive: false });
}

function initDesktopControls() {
    const sideNavItems = document.querySelectorAll('.side-nav-item');
    sideNavItems.forEach(item => {
        item.addEventListener('click', function(e) {
            e.preventDefault();
            
            sideNavItems.forEach(navItem => navItem.classList.remove('active'));
            
            this.classList.add('active');
            
            this.classList.add('animate-press');
            setTimeout(() => {
                this.classList.remove('animate-press');
            }, 200);
            
            const sectionName = this.querySelector('span').textContent;
            showToastNotification('Переход в раздел: ' + sectionName);
        });
    });
    
    const uploadButton = document.querySelector('.upload-button');
    if (uploadButton) {
        uploadButton.addEventListener('click', function() {
            showToastNotification('Загрузка видео');
            
            this.classList.add('animate-press');
            setTimeout(() => {
                this.classList.remove('animate-press');
            }, 200);
        });
    }
    
    const showMoreButton = document.querySelector('.show-more');
    if (showMoreButton) {
        showMoreButton.addEventListener('click', function() {
            this.classList.add('animate-press');
            setTimeout(() => {
                this.classList.remove('animate-press');
            }, 200);
            
            const subscriptionItems = document.querySelectorAll('.subscription-item');
            subscriptionItems.forEach((item, index) => {
                if (index > 4) {
                    item.classList.toggle('hidden');
                    if (!item.classList.contains('hidden')) {
                        item.classList.add('animate-pop-in');
                    }
                }
            });
            
            const icon = this.querySelector('i');
            if (icon.classList.contains('fa-chevron-down')) {
                icon.classList.remove('fa-chevron-down');
                icon.classList.add('fa-chevron-up');
                this.querySelector('span').textContent = 'Показать меньше';
                showToastNotification('Больше подписок');
            } else {
                icon.classList.remove('fa-chevron-up');
                icon.classList.add('fa-chevron-down');
                this.querySelector('span').textContent = 'Показать больше';
                showToastNotification('Меньше подписок');
            }
        });
    }
    
    initVideoControls();
}

function initSideNav() {
    const categoryTags = document.querySelectorAll('.category-tag');
    categoryTags.forEach(tag => {
        tag.addEventListener('click', function(e) {
            e.preventDefault();
            
            this.classList.add('animate-pulse');
            setTimeout(() => {
                this.classList.remove('animate-pulse');
            }, 800);
            
            const categoryName = this.querySelector('span').textContent;
            showToastNotification('Категория: ' + categoryName);
        });
    });
    
    const subscriptionItems = document.querySelectorAll('.subscription-item');
    subscriptionItems.forEach(item => {
        const avatar = item.querySelector('.subscription-avatar');
        
        item.addEventListener('mouseenter', function() {
            avatar.classList.add('animate-glow');
        });
        
        item.addEventListener('mouseleave', function() {
            avatar.classList.remove('animate-glow');
        });
        
        item.addEventListener('click', function(e) {
            e.preventDefault();
            const username = item.querySelector('span').textContent;
            showToastNotification('Профиль: ' + username);
        });
    });
}

function initVideoControls() {
    const likeButtons = document.querySelectorAll('.like-btn');
    likeButtons.forEach(btn => {
        btn.addEventListener('click', function() {
            const icon = this.querySelector('i');
            
            this.classList.toggle('active');
            
            if (this.classList.contains('active')) {
                icon.classList.add('animate-like');
                showToastNotification('Вам понравилось это видео');
            } else {
                icon.classList.remove('animate-like');
                showToastNotification('Вы убрали лайк');
            }
        });
    });
    
    const commentButtons = document.querySelectorAll('.comment-btn');
    commentButtons.forEach(btn => {
        btn.addEventListener('click', function() {
            this.classList.add('animate-press');
            setTimeout(() => {
                this.classList.remove('animate-press');
            }, 200);
            
            showToastNotification('Комментарии скоро будут доступны');
        });
    });
    
    const bookmarkButtons = document.querySelectorAll('.bookmark-btn');
    bookmarkButtons.forEach(btn => {
        btn.addEventListener('click', function() {
            const icon = this.querySelector('i');
            
            this.classList.toggle('active');
            
            if (this.classList.contains('active')) {
                icon.classList.add('animate-bookmark');
                showToastNotification('Видео добавлено в закладки');
            } else {
                icon.classList.remove('animate-bookmark');
                showToastNotification('Видео удалено из закладок');
            }
        });
    });
    
    const shareButtons = document.querySelectorAll('.share-btn');
    shareButtons.forEach(btn => {
        btn.addEventListener('click', function() {
            this.classList.add('animate-press');
            setTimeout(() => {
                this.classList.remove('animate-press');
            }, 200);
            
            showToastNotification('Поделиться видео');
        });
    });
    
    const authorControls = document.querySelectorAll('.tiktok-author');
    authorControls.forEach(control => {
        control.addEventListener('click', function() {
            this.classList.add('animate-press');
            setTimeout(() => {
                this.classList.remove('animate-press');
            }, 200);
            
            const authorName = this.closest('.tiktok-controls').parentElement.querySelector('.tiktok-user-info h3').textContent;
            showToastNotification('Профиль: ' + authorName);
        });
    });
    
    const subscribeButtons = document.querySelectorAll('.subscribe-btn');
    subscribeButtons.forEach(btn => {
        btn.addEventListener('click', function() {
            this.classList.toggle('subscribed');
            
            this.classList.add('animate-press');
            setTimeout(() => {
                this.classList.remove('animate-press');
            }, 200);
            
            if (this.classList.contains('subscribed')) {
                this.textContent = 'Подписан';
                this.innerHTML = '<i class="fas fa-check"></i> Подписан';
                
                showToastNotification('Вы подписались на этого пользователя');
            } else {
                this.textContent = 'Подписаться';
                this.innerHTML = '<i class="fas fa-plus"></i> Подписаться';
                
                showToastNotification('Вы отписались от этого пользователя');
            }
        });
    });
}

function initAnimations() {
    document.querySelectorAll('.video-card').forEach(card => {
        card.classList.add('animate-fade-in');
    });
    
    document.querySelectorAll('.nav-btn i').forEach(icon => {
        icon.classList.add('animate-icon-pulse');
    });
}


function addGlowEffects() {
    document.querySelectorAll('.side-nav-item.active i').forEach(icon => {
        icon.classList.add('animate-glow-text');
    });
    
    document.querySelectorAll('.tiktok-author-avatar').forEach(avatar => {
        avatar.parentElement.addEventListener('mouseenter', () => {
            avatar.classList.add('animate-glow');
        });
        
        avatar.parentElement.addEventListener('mouseleave', () => {
            avatar.classList.remove('animate-glow');
        });
    });
    
    const uploadButton = document.querySelector('.upload-button');
    if (uploadButton) {
        uploadButton.classList.add('animate-glow');
    }
} 