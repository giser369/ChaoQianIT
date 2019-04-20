
function createButtonDialog(title, width, height) {
    // 声明mask的宽度和高度（也即整个屏幕的宽度和高度）
    var w, h;
    // 可见区域宽度和高度
    var cw = document.body.clientWidth;
    var ch = document.body.clientHeight;
    // 正文的宽度和高度
    var sw = document.body.scrollWidth;
    var sh = document.body.scrollHeight;
    // 可见区域顶部距离body顶部和左边距离
    var st = document.body.scrollTop;
    var sl = document.body.scrollLeft;
    w = cw > sw ? cw : sw;
    h = ch > sh ? ch : sh;
    // 这是主窗体
    var win = createEle('div');
    win.id = "divRightButton";
    win.style.cssText = "position:fixed;right:" +2
	+ "px;top:" + (window.screen.availHeight - height) / 2 + "px;z-index:10001;width:" + width
			+ "px;height:" + height + "px;background-color: #c7edcc;";
    // 标题栏
    var tBar = createEle('div');
    tBar.style.cssText = "margin:0;text-align:center;width:100%;"
			+ "height:25px;cursor:move;background-color: #0062C4;";

    // 标题栏中的文字部分
    var titleCon = createEle('div');
    titleCon.style.cssText = "text-align:center;color:white;";
    titleCon.innerHTML = title;// firefox不支持innerText，所以这里用innerHTML
    tBar.appendChild(titleCon); // 将文字加入到标题栏中
    win.appendChild(tBar);

    // 窗体的内容部分，CSS中的overflow使得当内容大小超过此范围时，会出现滚动条
    var htmlCon = createEle('div');
    htmlCon.id = "divButtons";
    htmlCon.style.cssText = "text-align:center;width:" + width + "px;height:"
			+ (height - 50) + "px;";

    win.appendChild(htmlCon); // 将内容部分添加到主窗体中
    appChild(win);
}