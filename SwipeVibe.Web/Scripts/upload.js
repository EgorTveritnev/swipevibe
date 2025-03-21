document.addEventListener('DOMContentLoaded', function() {
    const dropzone = document.getElementById('dropzone');
    const videoUpload = document.getElementById('video-upload');
    const videoPreview = document.getElementById('video-preview');
    const videoPreviewContainer = document.querySelector('.video-preview-container');
    const submitBtn = document.querySelector('.submit-btn');
    const removeVideoBtn = document.getElementById('remove-video');
    const trimVideoBtn = document.getElementById('trim-video');
    const uploadProgress = document.querySelector('.upload-progress');
    const progressBar = document.querySelector('.progress-bar');
    const progressPercentage = document.querySelector('.progress-percentage');
    const videoTitle = document.getElementById('video-title');
    const videoDescription = document.getElementById('video-description');
    const videoTags = document.getElementById('video-tags');
    const categoryBtns = document.querySelectorAll('.category-btn');
    const cancelBtn = document.querySelector('.cancel-btn');
    const MAX_FILE_SIZE = 52428800;   
    initDragAndDrop();    
    initEventListeners();
    function initDragAndDrop() {
        dropzone.addEventListener('dragover', function(e) {
            e.preventDefault();
            dropzone.classList.add('drag-over');
        });
        dropzone.addEventListener('dragleave', function() {
            dropzone.classList.remove('drag-over');
        });
        dropzone.addEventListener('drop', function(e) {
            e.preventDefault();
            dropzone.classList.remove('drag-over');
            if (e.dataTransfer.files.length) {
                handleVideoFile(e.dataTransfer.files[0]);
            }
        });
    }
    
    function initEventListeners() {
        videoUpload.addEventListener('change', function() {
            if (this.files.length) {
                handleVideoFile(this.files[0]);
            }
        });
        removeVideoBtn.addEventListener('click', function() {
            resetUpload();
        });
        trimVideoBtn.addEventListener('click', function() {
            showToast('Функция обрезки видео в разработке', 'info');
        });
        videoTitle.addEventListener('input', validateForm);
        submitBtn.addEventListener('click', function(e) {
            if (!submitBtn.disabled) {
                simulateUpload();
            }
        });
        
        cancelBtn.addEventListener('click', function() {
            if (confirm('Вы уверены, что хотите отменить загрузку? Все внесенные данные будут потеряны.')) {
                resetUpload();
                videoTitle.value = '';
                videoDescription.value = '';
                videoTags.value = '';
                categoryBtns.forEach((btn, index) => {
                    btn.classList.toggle('active', index === 0);
                });
                document.querySelector('input[name="access"][value="public"]').checked = true;
            }
        });     
        categoryBtns.forEach(btn => {
            btn.addEventListener('click', function() {
                categoryBtns.forEach(b => b.classList.remove('active'));
                this.classList.add('active');
            });
        });
    }

    function handleVideoFile(file) {
        if (!file.type.startsWith('video/')) {
            showToast('Пожалуйста, выберите видеофайл', 'error');
            return;
        }
        
        if (file.size > MAX_FILE_SIZE) {
            showToast(`Размер файла превышает максимально допустимый (${MAX_FILE_SIZE / 1048576}МБ)`, 'error');
            return;
        }     
        const videoURL = URL.createObjectURL(file);
        videoPreview.src = videoURL;     
        dropzone.style.display = 'none';
        videoPreviewContainer.style.display = 'flex';     
        validateForm();  
        videoPreview.onloadedmetadata = function() {
            if (videoPreview.duration > 180) {
                showToast('Длительность видео не должна превышать 3 минуты', 'warning');
            }
        };
    }
    
    function resetUpload() {
        videoPreview.src = '';
        videoPreviewContainer.style.display = 'none';
        dropzone.style.display = 'flex';
        videoUpload.value = '';    
        uploadProgress.style.display = 'none';
        progressBar.style.width = '0%';
        submitBtn.disabled = true;
    }
    
    function validateForm() {
        if (videoPreview.src && videoTitle.value.trim() !== '') {
            submitBtn.disabled = false;
        } else {
            submitBtn.disabled = true;
        }
    }
  
    function simulateUpload() {
        uploadProgress.style.display = 'block';      
        submitBtn.disabled = true;       
        let progress = 0;
        const interval = setInterval(() => {
            progress += Math.random() * 10;
            if (progress >= 100) {
                progress = 100;
                clearInterval(interval);               
                setTimeout(() => {
                    showToast('Видео успешно загружено!', 'success');
                    setTimeout(() => {
                        window.location.href = 'index.html';
                    }, 2000);
                }, 500);
            }  
            progressBar.style.width = `${progress}%`;
            progressPercentage.textContent = `${Math.round(progress)}%`;
        }, 500);
    }
    function showToast(message, type = 'info') {
        const toast = document.createElement('div');
        toast.className = `toast-notification ${type}`;
        let icon = '';
        switch (type) {
            case 'success':
                icon = '<i class="fas fa-check-circle"></i>';
                break;
            case 'error':
                icon = '<i class="fas fa-exclamation-circle"></i>';
                break;
            case 'warning':
                icon = '<i class="fas fa-exclamation-triangle"></i>';
                break;
            default:
                icon = '<i class="fas fa-info-circle"></i>';
        }
        toast.innerHTML = `
            ${icon}
            <span>${message}</span>
        `;
        document.body.appendChild(toast);
        setTimeout(() => {
            toast.classList.add('show');
        }, 10);
        setTimeout(() => {
            toast.classList.remove('show');
            setTimeout(() => {
                document.body.removeChild(toast);
            }, 300);
        }, 3000);
    }
}); 