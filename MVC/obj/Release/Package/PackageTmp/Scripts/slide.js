(function($){
	// 焦点图 
    var $root = $('#show'),
        root_w = $root.width();
    var p = $root.find('>ul'),
        n = p.children().length;
    	p.children().eq(0).clone().appendTo(p);
    function onoff(on, off) {
        (on !== -1) && btns.eq(on).addClass('on');
        (off !== -1) && btns.eq(off).removeClass('on');
    }
    function dgo(n, comp) {
        var idx = n > max ? 0 : n;
        onoff(idx, cur);
        cur = idx;
        p.stop().animate({left: -1 * root_w * n}, {duration: dur, complete: comp});
    }
    // slast -> 如果播放完最后1张，要如何处理
    //    true 平滑切换到第1张
    var cur = 0,
        max = n - 1,
        pt = 0,
        stay = 1 * 1000, /* ms */
        dur = .3 * 1000, /* ms */
        btns;
    function go(dir, slast) {
        pt = +new Date();
        if (dir === 0) {
            onoff(cur, -1);
            p.css({left: -1 * root_w * cur});
            return;
        }
        var t;
        if (dir > 0) {
            t = cur + 1;
            if (t > max && !slast) {
                t = 0;
            }
            if (t <= max) {
                return dgo(t);
            }
            return dgo(t, function(){
                p.css({left: 0});
            });
        } else {
            t = cur - 1;
            if (t < 0) {
                t = max;
                p.css({left: -1 * root_w * (max + 1)});
                return dgo(t);
            } else {
                return dgo(t);
            }
        }
    }
    btns = $((new Array(n + 1)).join('<i></i>'))
        .each(function(idx, el) {
            $(el).data({idx: idx});
     });
    $('<div class="btns"/ >')
        .append(
            $('<b/>')
                .append(btns)
                .delegate('i', 'click', function(ev) {
                    dgo($(this).data('idx'));
                })
        ).appendTo($root);
    go(1);
    // 自动播放
    if (true) {
       var si = setInterval(function(){
            var now = +new Date();
            if (now - pt < stay) {
                return;
            }
            go(1, true);
        }, 3000);
			 p.mouseover(function(){ clearInterval(si);})
			 p.mouseout(function(){
						si = setInterval(function(){
            var now = +new Date();
            if (now - pt < stay) {
                return;
            }
            go(1, true);
        }, 3000);})
    }
})(jQuery);