//初始化文章标题树
function initArticleTree() {
    var uri = '/api/articletile';

    // 图层树初始化
    var initDevicetree = {
        view: {
            open:true,
            // 是否可以同时选中多个节点
            selectedMulti: false,
            // 字体颜色
            fontCss: {
                color: "black"
            }
        },
        data: {
            // 是否使用简单说数据格式
            simpleData: {
                enable: true
            }
        },
        // 异步信息设置
        async: {
            enable: true,
            // 请求方式
            type: "get",
            // 返回结果类型
            dataType: "json",
            // 请求地址
            url: uri
        },
        // 勾选框
        check: {
            enable: false,
            // 勾选框类型
            chkStyle: "checkbox",
            // 勾选设置
            chkboxType: {
                "Y": "s",
                "N": "s"
            }
        },
        callback: {
            // 点击事件
            onClick: onArticleTileNodeClick
            // 异步请求前
            // beforeAsync : LayerzTreeBefore
        }
    };
    // 初始化图层树
    var $j = jQuery.noConflict();
    $j.fn.zTree.init($("#treeDemo"), initDevicetree);
}

function onArticleTileNodeClick(event, treeId, treeNode) {
}
