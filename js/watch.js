document.addEventListener('DOMContentLoaded', function() {
    showToast('Страница "Смотреть" загружена', 'success');
    
    const glowElements = document.querySelectorAll('.animate-glow-text');
    glowElements.forEach(element => {
        element.style.textShadow = '0 0 10px rgba(106, 103, 206, 0.6)';
    });
    setupFilterButtons();
    setupVideoCards();
    setupLoadMoreButton();
});


function setupFilterButtons() {
    const filterButtons = document.querySelectorAll('.filter-btn');
    
    filterButtons.forEach(button => {
        button.addEventListener('click', function() {
            filterButtons.forEach(btn => btn.classList.remove('active'));
            this.classList.add('active');
            const activeIcon = this.querySelector('i');
            if (activeIcon) {
                activeIcon.classList.add('animate-glow-text');
            }
            const filterText = this.textContent.trim();
            showToast(`Выбрана категория: ${filterText}`, 'info');
            simulateFilteredContent(filterText);
        });
    });
    
    const sortButton = document.querySelector('.filter-control-btn:first-child');
    const filterControlButton = document.querySelector('.filter-control-btn:last-child');
    
    sortButton.addEventListener('click', function() {
        showToast('Сортировка по популярности', 'info');
        this.classList.toggle('active');
    });
    filterControlButton.addEventListener('click', function() {
        showToast('Дополнительные фильтры', 'info');
        this.classList.toggle('active');
    });
}


function setupVideoCards() {
    const videoItems = document.querySelectorAll('.video-item');
    
    videoItems.forEach(item => {
        item.addEventListener('click', function(e) {
            if (e.target.closest('.play-btn')) {
                e.preventDefault();
                const videoTitle = this.querySelector('.video-title').textContent;
                showToast(`Воспроизведение: ${videoTitle}`, 'info');
                simulateVideoPlayback(this);
            } else {
                const videoTitle = this.querySelector('.video-title').textContent;
                showToast(`Открытие видео: ${videoTitle}`, 'info');
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
function setupLoadMoreButton() {
    const loadMoreBtn = document.querySelector('.load-more-btn');
    
    if (loadMoreBtn) {
        loadMoreBtn.addEventListener('click', function() {
            showToast('Загрузка дополнительного контента...', 'info');
            const originalText = this.innerHTML;
            this.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Загрузка...';
            this.disabled = true;
            this.style.boxShadow = '0 0 15px rgba(106, 103, 206, 0.4)';
            this.style.background = 'rgba(106, 103, 206, 0.3)';
            setTimeout(() => {
                this.innerHTML = originalText;
                this.disabled = false;
                this.style.boxShadow = '';
                this.style.background = '';
                addMoreVideos();
                showToast('Загружено больше видео', 'success');
            }, 1500);
        });
    }
}

function showToast(message, type = 'info') {
    if (window.showToastNotification) {
        window.showToastNotification(message, type);
    } else {
        const toastContainer = document.querySelector('.toast-container');
        const toast = document.createElement('div');
        toast.className = `toast toast-${type}`;
        toast.innerHTML = `
            <div class="toast-with-icon">
                <div class="toast-icon">
                    <i class="fas ${getIconForToastType(type)}"></i>
                </div>
                <div class="toast-content">${message}</div>
            </div>
        `;
        toast.style.background = 'rgba(14, 14, 18, 0.95)';
        toast.style.boxShadow = '0 0 15px rgba(0, 0, 0, 0.5), 0 0 10px rgba(106, 103, 206, 0.3)';
        toast.style.backdropFilter = 'blur(10px)';
        toast.style.border = '1px solid rgba(106, 103, 206, 0.2)';
        toastContainer.appendChild(toast);    
        setTimeout(() => toast.classList.add('show'), 10);
        setTimeout(() => {
            toast.classList.remove('show');
            toast.classList.add('hide');
            setTimeout(() => {
                toast.remove();
            }, 300);
        }, 3000);
    }
}


function getIconForToastType(type) {
    switch (type) {
        case 'success':
            return 'fa-check-circle';
        case 'error':
            return 'fa-exclamation-circle';
        case 'warning':
            return 'fa-exclamation-triangle';
        case 'info':
        default:
            return 'fa-info-circle';
    }
}


function simulateFilteredContent(category) {
    const videoItems = document.querySelectorAll('.video-item');
    videoItems.forEach(item => {
        item.style.opacity = '0.5';
        item.style.transform = 'scale(0.95)';
        item.style.boxShadow = '-5px -5px 10px rgba(20, 20, 20, 0.5), 5px 5px 10px rgba(0, 0, 0, 0.3)';
    });
    setTimeout(() => {
        videoItems.forEach(item => {
            item.style.opacity = '1';
            item.style.transform = 'translateY(0)';
            item.style.boxShadow = '';
        });
        
    }, 500);
}


function simulateVideoPlayback(videoCard) {
    videoCard.style.boxShadow = '-8px -8px 16px rgba(20, 20, 20, 0.6), 8px 8px 16px rgba(0, 0, 0, 0.4), 0 0 20px rgba(106, 103, 206, 0.5)';
    videoCard.style.borderColor = 'rgba(106, 103, 206, 0.3)';
    
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

function addMoreVideos() {
    const videoGrid = document.querySelector('.video-grid');

    const existingVideos = document.querySelectorAll('.video-item');
    
    for (let i = 0; i < 4; i++) {
        const randomIndex = Math.floor(Math.random() * existingVideos.length);
        const videoClone = existingVideos[randomIndex].cloneNode(true);
        
        const videoTitle = videoClone.querySelector('.video-title');
        videoTitle.textContent = 'Новое видео: ' + videoTitle.textContent;
        
        videoClone.style.opacity = '0';
        videoClone.style.transform = 'translateY(20px)';
        videoClone.style.boxShadow = '-8px -8px 16px rgba(20, 20, 20, 0.6), 8px 8px 16px rgba(0, 0, 0, 0.4)';
        videoClone.style.border = '1px solid rgba(106, 103, 206, 0.2)';
        
        const thumbnail = videoClone.querySelector('.video-thumbnail');
        if (thumbnail && !thumbnail.classList.contains('new') && !thumbnail.classList.contains('trending')) {
            thumbnail.classList.add('new');
            const newBadge = document.createElement('div');
            newBadge.className = 'new-badge';
            newBadge.innerHTML = '<i class="fas fa-certificate"></i> Новое';
            thumbnail.appendChild(newBadge);
        }
        videoGrid.appendChild(videoClone);
        
        setTimeout(() => {
            videoClone.style.opacity = '1';
            videoClone.style.transform = 'translateY(0)';
        }, i * 100);
    }
    
    setupVideoCards();
} 