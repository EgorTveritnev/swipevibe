
document.addEventListener('DOMContentLoaded', function() {
    initFriendsPage();
});


function initFriendsPage() {
    initControlTabs();
    
    initFriendSearch();
    
    initActionButtons();
    
    initShowMoreButton();
    
    initTooltips();
    
    initFilters();
    
    simulateDataLoading();
}


function initFilters() {
    const filterBtn = document.querySelector('.filter-btn');
    const filterDropdown = document.querySelector('.filter-dropdown-menu');
    const closeFilterBtn = document.querySelector('.close-filter');
    const applyFiltersBtn = document.querySelector('.apply-filters');
    const resetFiltersBtn = document.querySelector('.reset-filters');
    
    if (filterBtn && filterDropdown) {
        filterBtn.addEventListener('click', function(e) {
            e.stopPropagation();
            filterDropdown.classList.toggle('show');
            
            this.classList.add('animate-glow');
            setTimeout(() => {
                this.classList.remove('animate-glow');
            }, 500);
        });
    }
    
    if (closeFilterBtn && filterDropdown) {
        closeFilterBtn.addEventListener('click', function() {
            filterDropdown.classList.remove('show');
        });
    }
    
    if (applyFiltersBtn && filterDropdown) {
        applyFiltersBtn.addEventListener('click', function() {
            filterDropdown.classList.remove('show');
            
            const statusFilters = document.querySelectorAll('.filter-option input[type="checkbox"]:checked');
            const sortFilter = document.querySelector('.filter-option input[type="radio"]:checked');
            
            let statusFilterText = 'Статус: ';
            statusFilters.forEach((filter, index) => {
                statusFilterText += filter.parentElement.textContent.trim();
                if (index < statusFilters.length - 1) {
                    statusFilterText += ', ';
                }
            });
            
            let sortFilterText = 'Сортировка: ' + (sortFilter ? sortFilter.parentElement.textContent.trim() : 'По умолчанию');
            
            showToast('Фильтры применены');
            showToast(statusFilterText);
            showToast(sortFilterText);
        });
    }
    
    if (resetFiltersBtn) {
        resetFiltersBtn.addEventListener('click', function() {
            const checkboxes = document.querySelectorAll('.filter-option input[type="checkbox"]');
            const firstRadio = document.querySelector('.filter-option input[type="radio"]');
            
            checkboxes.forEach(checkbox => {
                checkbox.checked = true;
            });
            
            if (firstRadio) {
                firstRadio.checked = true;
            }
            
            showToast('Фильтры сброшены');
        });
    }
    
    document.addEventListener('click', function(e) {
        if (filterDropdown && filterDropdown.classList.contains('show')) {
            if (!filterDropdown.contains(e.target) && e.target !== filterBtn) {
                filterDropdown.classList.remove('show');
            }
        }
    });
}


function initControlTabs() {
    const tabs = document.querySelectorAll('.control-tab');
    
    tabs.forEach(tab => {
        tab.addEventListener('click', function() {
            tabs.forEach(t => t.classList.remove('active'));
            
            this.classList.add('active');
            
            const tabType = this.textContent.trim().includes('Запросы') ? 'requests' : 
                            this.textContent.trim().includes('Найти') ? 'find' : 'all';
            
            simulateTabChange(tabType);
            
            showToast(`Выбрана вкладка "${this.textContent.trim()}"`);
        });
    });
}


function initFriendSearch() {
    const searchInput = document.querySelector('.friend-search input');
    const filterBtn = document.querySelector('.filter-btn');
    
    if (searchInput) {
        searchInput.addEventListener('input', function() {
            const searchTerm = this.value.toLowerCase();
            const friendCards = document.querySelectorAll('.friend-card');
            
            friendCards.forEach(card => {
                const friendName = card.querySelector('.friend-name').textContent.toLowerCase();
                const friendUsername = card.querySelector('.friend-username').textContent.toLowerCase();
                
                if (friendName.includes(searchTerm) || friendUsername.includes(searchTerm)) {
                    card.style.display = 'flex';
                } else {
                    card.style.display = 'none';
                }
            });
        });
    }
    
    if (filterBtn) {
        filterBtn.addEventListener('click', function() {
            showToast('Открыты настройки фильтрации');
            
            this.classList.add('animate-glow');
            setTimeout(() => {
                this.classList.remove('animate-glow');
            }, 500);
        });
    }
}


function initActionButtons() {
    const messageButtons = document.querySelectorAll('.message-btn');
    messageButtons.forEach(btn => {
        btn.addEventListener('click', function(e) {
            e.stopPropagation();
            const friendCard = this.closest('.friend-card');
            const friendName = friendCard.querySelector('.friend-name').textContent;
            
            showToast(`Открыт чат с ${friendName}`);
            
            this.classList.add('animate-glow');
            setTimeout(() => {
                this.classList.remove('animate-glow');
            }, 500);
        });
    });

    const videoCallButtons = document.querySelectorAll('.video-call-btn');
    videoCallButtons.forEach(btn => {
        btn.addEventListener('click', function(e) {
            e.stopPropagation();
            const friendCard = this.closest('.friend-card');
            const friendName = friendCard.querySelector('.friend-name').textContent;
            
            showToast(`Звонок пользователю ${friendName}`);
            
            this.classList.add('animate-glow');
            setTimeout(() => {
                this.classList.remove('animate-glow');
            }, 500);
        });
    });
    
    const watchStreamButtons = document.querySelectorAll('.watch-stream-btn');
    watchStreamButtons.forEach(btn => {
        btn.addEventListener('click', function(e) {
            e.stopPropagation();
            const friendCard = this.closest('.friend-card');
            const friendName = friendCard.querySelector('.friend-name').textContent;
            
            showToast(`Открыта трансляция ${friendName}`);
            
            this.classList.add('animate-pulse');
            setTimeout(() => {
                this.classList.remove('animate-pulse');
            }, 800);
        });
    });
    
    const addFriendButtons = document.querySelectorAll('.add-friend-btn');
    addFriendButtons.forEach(btn => {
        btn.addEventListener('click', function(e) {
            e.stopPropagation();
            const recommendationItem = this.closest('.recommendation-item');
            const friendName = recommendationItem.querySelector('h4').textContent;
            
            showToast(`Отправлен запрос в друзья пользователю ${friendName}`);
            
            this.innerHTML = '<i class="fas fa-check"></i>';
            this.style.backgroundColor = 'rgba(76, 175, 80, 0.1)';
            this.style.color = '#4CAF50';
            this.disabled = true;
        });
    });
    
    const friendCards = document.querySelectorAll('.friend-card');
    friendCards.forEach(card => {
        card.addEventListener('click', function() {
            const friendName = this.querySelector('.friend-name').textContent;
            showToast(`Открыт профиль ${friendName}`);
        });
    });
}


function initShowMoreButton() {
    const showMoreBtn = document.querySelector('.show-more-friends');
    
    if (showMoreBtn) {
        showMoreBtn.addEventListener('click', function() {
            this.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Загрузка...';
            
            setTimeout(() => {
                this.innerHTML = '<i class="fas fa-chevron-down"></i> Показать больше';
                showToast('Загружены дополнительные друзья');
            }, 1500);
        });
    }
}


function initTooltips() {
    const actionButtons = document.querySelectorAll('.action-btn');
    
    actionButtons.forEach(btn => {
        if (btn.classList.contains('message-btn')) {
            btn.setAttribute('title', 'Написать сообщение');
        } else if (btn.classList.contains('video-call-btn')) {
            btn.setAttribute('title', 'Видеозвонок');
        } else if (btn.classList.contains('watch-stream-btn')) {
            btn.setAttribute('title', 'Смотреть трансляцию');
        }
    });
}

function showToast(message) {
    const toastContainer = document.querySelector('.toast-container');
    
    const toast = document.createElement('div');
    toast.className = 'toast-notification neomorph-card';
    
    toast.innerHTML = `
        <div class="toast-icon">
            <i class="fas fa-info-circle"></i>
        </div>
        <div class="toast-message">${message}</div>
    `;
    
    toastContainer.appendChild(toast);
    
    setTimeout(() => {
        toast.classList.add('show');
    }, 10);
    
    setTimeout(() => {
        toast.classList.remove('show');
        setTimeout(() => {
            toast.remove();
        }, 300);
    }, 3000);
}


function simulateTabChange(tabType) {
    const friendsCategories = document.querySelectorAll('.friends-category');
    const recommendationsSection = document.querySelector('.friends-recommendations');
    switch (tabType) {
        case 'requests':
            friendsCategories.forEach(cat => cat.style.display = 'none');
            recommendationsSection.style.display = 'none';
            break;
            
        case 'find':
            friendsCategories.forEach(cat => cat.style.display = 'none');
            recommendationsSection.style.display = 'block';
            break;
            
        default:
            friendsCategories.forEach(cat => cat.style.display = 'block');
            recommendationsSection.style.display = 'block';
            break;
    }
}


function simulateDataLoading() {
    showToast('Загрузка списка друзей...');
    
    setTimeout(() => {
        const friendsContent = document.querySelector('.friends-content');
        friendsContent.style.opacity = '0';
        friendsContent.style.transform = 'translateY(20px)';
        
        setTimeout(() => {
            friendsContent.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
            friendsContent.style.opacity = '1';
            friendsContent.style.transform = 'translateY(0)';
            
            showToast('Список друзей загружен');
        }, 200);
    }, 1000);
} 