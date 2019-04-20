// 创建元素
function createEle(tagName) {
    return document.createElement(tagName);
}
// 在body中添加子元素
function appChild(eleName) {
    return document.body.appendChild(eleName);
}
// 从body中移除子元素
function remChild(eleName) {
    return document.body.removeChild(eleName);
}