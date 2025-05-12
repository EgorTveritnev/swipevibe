document.addEventListener('DOMContentLoaded', function() {
    const activityCtx = document.createElement('canvas');
    activityCtx.id = 'activityChart';
    document.querySelector('.chart-container').appendChild(activityCtx);
    
    new Chart(activityCtx, {
        type: 'line',
        data: {
            labels: ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вс'],
            datasets: [{
                label: 'Активные пользователи',
                data: [1200, 1900, 1500, 2100, 1800, 2500, 2200],
                borderColor: '#6c5ce7',
                backgroundColor: 'rgba(108, 92, 231, 0.1)',
                tension: 0.4,
                fill: true
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    display: false
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    grid: {
                        color: 'rgba(255, 255, 255, 0.1)'
                    },
                    ticks: {
                        color: '#a0a0a0'
                    }
                },
                x: {
                    grid: {
                        display: false
                    },
                    ticks: {
                        color: '#a0a0a0'
                    }
                }
            }
        }
    });
    
    const categoriesCtx = document.createElement('canvas');
    categoriesCtx.id = 'categoriesChart';
    document.querySelectorAll('.chart-container')[1].appendChild(categoriesCtx);
    
    new Chart(categoriesCtx, {
        type: 'doughnut',
        data: {
            labels: ['Музыка', 'Танцы', 'Юмор', 'Спорт', 'Образование'],
            datasets: [{
                data: [30, 25, 20, 15, 10],
                backgroundColor: [
                    '#6c5ce7',
                    '#00b894',
                    '#fdcb6e',
                    '#e17055',
                    '#0984e3'
                ]
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'right',
                    labels: {
                        color: '#a0a0a0'
                    }
                }
            }
        }
    });
});
const searchInput = document.querySelector('.header-search input');
searchInput.addEventListener('input', function(e) {
    const searchTerm = e.target.value.toLowerCase();
    console.log('Поиск:', searchTerm);
});

const notificationBtn = document.querySelector('.notification-btn');
notificationBtn.addEventListener('click', function() {
    console.log('Открыть уведомления');
});

const adminProfile = document.querySelector('.admin-profile');
adminProfile.addEventListener('click', function() {
    console.log('Открыть меню профиля');
});

const statCards = document.querySelectorAll('.stat-card');
statCards.forEach(card => {
    card.addEventListener('mouseenter', function() {
        this.style.transform = 'translateY(-5px)';
    });
    
    card.addEventListener('mouseleave', function() {
        this.style.transform = 'translateY(0)';
    });
});

const navItems = document.querySelectorAll('.admin-nav-item');
navItems.forEach(item => {
    item.addEventListener('mouseenter', function() {
        if (!this.classList.contains('active')) {
            this.style.transform = 'translateX(5px)';
        }
    });
    
    item.addEventListener('mouseleave', function() {
        this.style.transform = 'translateX(0)';
    });
}); 