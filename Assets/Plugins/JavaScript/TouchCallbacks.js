// 注册触摸事件的回调函数
function RegisterTouchCallbacks() {
    var canvas = document.getElementById("canvas"); // 获取Canvas元素
    canvas.addEventListener('touchstart', function(e) {
        var touch = e.touches[0]; // 获取第一个触点
        TouchStart(touch.clientX, touch.clientY); // 触发Unity中的TouchStart函数
        e.preventDefault();
    });
 
    canvas.addEventListener('touchmove', function(e) {
        var touch = e.touches[0];
        TouchMove(touch.clientX, touch.clientY); // 触发Unity中的TouchMove函数
        e.preventDefault();
    });
 
    canvas.addEventListener('touchend', function(e) {
        var touch = e.touches[0];
        TouchEnd(touch.clientX, touch.clientY); // 触发Unity中的TouchEnd函数
        e.preventDefault();
    });
}
 
// 这些函数需要与Unity中的函数签名相匹配
function TouchStart(x, y) {
    UnityLoader.SystemInfo.hasWebGL ? UnityInstance.SendMessage('MainOBj', 'TouchStart', x + ',' + y) : null;
}
 
function TouchMove(x, y) {
    UnityLoader.SystemInfo.hasWebGL ? UnityInstance.SendMessage('MainOBj', 'TouchMove', x + ',' + y) : null;
}
 
function TouchEnd(x, y) {
    UnityLoader.SystemInfo.hasWebGL ? UnityInstance.SendMessage('MainOBj', 'TouchEnd', x + ',' + y) : null;
}
