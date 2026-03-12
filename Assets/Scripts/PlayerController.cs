using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    // 公有变量：玩家移动速度，以及 UI 文本对象
    public float speed;
    public Text countText;
    public Text winText;

    // 私有变量：玩家刚体引用，以及得分计数
    private Rigidbody rb;
    private int count;

    // 游戏开始时执行
    void Start()
    {
        // 获取玩家物体上的 Rigidbody 组件
        rb = GetComponent<Rigidbody>();

        // 计数器清零
        count = 0;

        // 更新 UI 显示
        SetCountText();

        // 初始时胜利文字为空
        winText.text = "";
    }

    // 固定频率更新，用于处理物理逻辑
    void FixedUpdate()
    {
        // 获取键盘/手柄的水平和垂直输入
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 创建移动向量 (X 轴和 Z 轴移动，Y 轴不动)
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // 给刚体施加力
        rb.AddForce(movement * speed);
    }

    // --- 重点修改部分 ---
    // 当球体碰到“实体”碰撞体（没有勾选 Is Trigger 的物体）时执行
    void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞到的物体是否带有 'Pick Up' 标签
        if (collision.gameObject.CompareTag("Pick Up"))
        {
            // 让该物体消失
            collision.gameObject.SetActive(false);

            // 增加分数
            count = count + 1;

            // 更新分数 UI
            SetCountText();
        }
    }

    // 更新分数的独立函数
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        // 如果分数达到 13（你可以根据你的食物总数修改这个数值）
        if (count >= 13)
        {
            winText.text = "You Win!";
        }
    }
}