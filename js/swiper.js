document.addEventListener('DOMContentLoaded', () => {
    console.log('Swiper initialized');  
    initSwiper();
});

function initSwiper() {
    const videoContainer = document.querySelector('.video-container');
    let touchStartY = 0;
    let touchEndY = 0;
    let touchStartX = 0;
    let touchEndX = 0;
    const minSwipeDistance = 100;    
    videoContainer.addEventListener('touchstart', (e) => {
        touchStartY = e.changedTouches[0].screenY;
        touchStartX = e.changedTouches[0].screenX;
    });
    
    videoContainer.addEventListener('touchend', (e) => {
        touchEndY = e.changedTouches[0].screenY;
        touchEndX = e.changedTouches[0].screenX;        
        const swipeDistanceY = touchEndY - touchStartY;
        const swipeDistanceX = touchEndX - touchStartX;
        if (Math.abs(swipeDistanceY) > Math.abs(swipeDistanceX)) {
            handleVerticalSwipe(swipeDistanceY);
        } else {
            handleHorizontalSwipe(swipeDistanceX);
        }
    });
    
    videoContainer.addEventListener('mousedown', (e) => {
        e.preventDefault();
        touchStartY = e.clientY;
        touchStartX = e.clientX;      
        const handleMouseUp = (e) => {
            touchEndY = e.clientY;
            touchEndX = e.clientX;          
            const swipeDistanceY = touchEndY - touchStartY;
            const swipeDistanceX = touchEndX - touchStartX;           
            if (Math.abs(swipeDistanceY) > Math.abs(swipeDistanceX)) {
                handleVerticalSwipe(swipeDistanceY);
            } else {
                handleHorizontalSwipe(swipeDistanceX);
            }           
            document.removeEventListener('mouseup', handleMouseUp);
        };       
        document.addEventListener('mouseup', handleMouseUp);
    });
    
    videoContainer.addEventListener('wheel', (e) => {
        e.preventDefault();       
        if (e.deltaY > 0) {
            showNextVideo();
        } 
        else if (e.deltaY < 0) {
            showPrevVideo();
        }
    }, { passive: false });   
    document.addEventListener('keydown', (e) => {
        if (e.key === 'ArrowUp') {
            showPrevVideo();
        } else if (e.key === 'ArrowDown') {
            showNextVideo();
        }
    });
}

function handleVerticalSwipe(swipeDistance) {
    const minSwipeDistance = 100;   
    if (Math.abs(swipeDistance) < minSwipeDistance) {
        return;
    }
    
    if (swipeDistance < 0) {
        showNextVideo();
    } else {
        showPrevVideo();
    }
}

function handleHorizontalSwipe(swipeDistance) {
    const minSwipeDistance = 100;   
    if (Math.abs(swipeDistance) < minSwipeDistance) {
        return;
    }    
    if (swipeDistance < 0) {
        const likeButton = document.querySelector('.video-card.active .like-btn');
        if (likeButton) {
            likeButton.click();
        }
    } else {
        const commentButton = document.querySelector('.video-card.active .comment-btn');
        if (commentButton) {
            alert('Комментарии будут доступны в следующем обновлении');
        }
    }
} 