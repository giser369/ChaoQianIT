// 弹出窗口，标题（html形式）、宽度、高度、是否为模式对话框(true,false)
function createPrompt(title, width, height) {
    // 避免窗体过小
    if (width < 300) {
        width = 220;
    }
    if (height < 200) {
        height = 200;
    }
    // 声明mask的宽度和高度（也即整个屏幕的宽度和高度）
    var w, h;
    var divLeftConWidth = 200; //内容部分左侧区域的宽度
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
    // 避免窗体过大
    if (width > w) {
        width = w;
    }
    if (height > h) {
        height = h;
    }
    // 这是主窗体
    var win = createEle('div');
    win.id = "maindiv";
    win.style.cssText = "position:fixed;left:" +2
	+ "px;bottom:2" + "px;z-index:10001;width:" + width
			+ "px;height:" + height + "px;background-color: white;";
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
    htmlCon.id = "divContent";
    htmlCon.style.cssText = "text-align:center;width:" + width + "px;height:"
			+ (height - 50) + "px;";

    // 内容部分的左边区域，用来显示图片列表
    var leftDiv = createEle("div");
    leftDiv.id = "divLeft";
    leftDiv.innerHTML = "今日关注！！！";
    leftDiv.style.cssText = "width:" + width
			+ "px;overflow:auto;font-size:15px;color:red;";
    htmlCon.appendChild(leftDiv);

    // 内容部分的右边区域，用来展示图片
    var rightDiv = createEle("div");
    rightDiv.id = "divImage";
    rightDiv.style.cssText = "width:" + width + "px;";
    var imgCon = createEle("img");
    imgCon.id = "imgCon";
    imgCon.src = "/Content/love.jpg";
    imgCon.style.cssText = "width:100%;height:100%";
    rightDiv.appendChild(imgCon);
    htmlCon.appendChild(rightDiv);

    win.appendChild(htmlCon); // 将内容部分添加到主窗体中
    appChild(win);
}