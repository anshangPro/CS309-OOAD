using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[Serializable]
public enum TextAnimationType
{
    None = 0,
    Normal = 1,
    Burst = 2
}
public enum TextMoveType
{
    None,
    Up,
    Down,
    Left,
    Right,
    LeftUp,
    LeftDown,
    RightUp,
    RightDown,
    LeftParabola,
    RightParabola
}
public class ShootTextProController : MonoBehaviour
{
    private readonly string operatorPlusKeyPostfix = "_operator_plus";
    private readonly string operatorMinusKeyPostfix = "_operator_minus";
    private readonly string numberPrefix = "_NumberImage_";
    public Dictionary<string, GameObject> numberDic = new Dictionary<string, GameObject>();

    private List<ShootTextComponent> handleShootTextGroup = new List<ShootTextComponent>();
    private Queue<ShootTextInfo> waitShootTextGroup = new Queue<ShootTextInfo>();
    private List<ShootTextComponent> waitDestoryGroup = new List<ShootTextComponent>();

    private Transform shootTextCanvas = null;
    public Transform ShootTextCanvas
    {
        get
        {
            if (shootTextCanvas == null)
            {
                shootTextCanvas = GameObject.Find("Canvas").transform;
            }
            if (shootTextCanvas == null)
            {
            }
            return shootTextCanvas;
        }
        set { shootTextCanvas = value; }
    }
    private Camera shootTextCamera = null;
    public Camera ShootTextCamera
    {
        get
        {
            return shootTextCamera == null ? Camera.main : shootTextCamera;
        }
        set
        {
            shootTextCamera = value;
        }
    }

    [Header("渐隐曲线")]
    [SerializeField]
    private AnimationCurve shootTextCure = null;
    [Header("抛物线曲线")]
    [SerializeField]
    private AnimationCurve shootParabolaCure = null;

    [Header("最大等待弹射数量")]
    [SerializeField]
    private int MaxWaitCount = 20;
    [Header("超过此数量弹射加速")]
    [SerializeField]
    private int accelerateThresholdValue = 10;
    [Header("加速弹射速率因子")]
    [SerializeField]
    private float accelerateFactor = 2;
    private bool isAccelerate = false;
    [Header("默认创建周期：秒/一次")]
    [SerializeField]
    private float updateCreatDefualtTime = 0.2f;
    //[SerializeField]
    private float updateCreatTime = 0.2f;
    //[SerializeField]
    private float updateCreatTempTime;
    [Header("从移动到消失的生命周期 单位：秒")]
    [SerializeField]
    private float moveLifeTime = 1.0f;

    [Header("远近缩放因子")]
    [SerializeField]
    private float shootTextScaleFactor = 0.6f;

    [Header("等待指定时间后开始移动")]
    [SerializeField]
    private float delayMoveTime = 0.3f;

    public float DelayMoveTime { get { return delayMoveTime; } set { delayMoveTime = value; } }
    [Range(-4, 4)]
    [Header("初始化位置垂直偏移量")]
    [SerializeField]
    private float initializedVerticalPositionOffset = 0.8f;
    [Range(-4, 4)]
    [Header("初始化位置水平偏移量")]
    [SerializeField]
    private float initializedHorizontalPositionOffset = 0.0f;
    [Header("垂直移动速率")]
    [Range(0, 20)]
    [SerializeField]
    private float verticalMoveSpeed = 10;
    [Header("水平移动速率")]
    [Range(0, 20)]
    [SerializeField]
    private float horizontalMoveSpeed = 10;
    [Header("字体动画类型")]
    public TextAnimationType textAnimationType;
    [Header("字体移动类型")]
    public TextMoveType textMoveType;

    /// <summary>
    /// 一次飘字UI父节点
    /// </summary>
    private GameObject shootTextPrefab = null;
    void Start()
    {
        Initialized();
    }
    private void Initialized()
    {
        shootTextCure = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1f), new Keyframe(moveLifeTime, 0f) });
        shootTextPrefab = Resources.Load<GameObject>("Prefabs/ShootText_Pure");
        updateCreatTempTime = updateCreatTime;
        //加法图片
        numberDic.Add(TextAnimationType.Normal.ToString() + operatorPlusKeyPostfix, Resources.Load<GameObject>("Prefabs/" + TextAnimationType.Normal.ToString() + operatorPlusKeyPostfix));
        numberDic.Add(TextAnimationType.Burst.ToString() + operatorPlusKeyPostfix, Resources.Load<GameObject>("Prefabs/" + TextAnimationType.Burst.ToString() + operatorPlusKeyPostfix));
        //减法图片
        numberDic.Add(TextAnimationType.Normal.ToString() + operatorMinusKeyPostfix, Resources.Load<GameObject>("Prefabs/" + TextAnimationType.Normal.ToString() + operatorMinusKeyPostfix));
        numberDic.Add(TextAnimationType.Burst.ToString() + operatorMinusKeyPostfix, Resources.Load<GameObject>("Prefabs/" + TextAnimationType.Burst.ToString() + operatorMinusKeyPostfix));

        for (int i = 0; i < 10; i++)
        {
            numberDic.Add(TextAnimationType.Normal.ToString() + numberPrefix + i, Resources.Load<GameObject>("Prefabs/" + TextAnimationType.Normal.ToString() + numberPrefix + i));
        }
        for (int i = 0; i < 10; i++)
        {
            numberDic.Add(TextAnimationType.Burst.ToString() + numberPrefix + i, Resources.Load<GameObject>("Prefabs/" + TextAnimationType.Burst.ToString() + numberPrefix + i));
        }
    }
    void Update()
    {

        float deltaTime = Time.deltaTime;

        //操作handleShootTextGroup中移动
        for (int i = 0; i < handleShootTextGroup.Count; i++)
        {
            ShootTextComponent shootTextComponent = handleShootTextGroup[i];

            Vector3 shootTextCreatPosition = Vector3.zero;
            shootTextCreatPosition = shootTextComponent.cacheTranform.GetComponent<Collider>().bounds.center + (((Vector3.up * shootTextComponent.cacheTranform.GetComponent<Collider>().bounds.size.y) * 0.5f));
            shootTextCreatPosition.x += (float)shootTextComponent.initializedHorizontalPositionOffset;
            shootTextCreatPosition.y += (float)shootTextComponent.initializedVerticalPositionOffset;

            Vector2 anchors = ShootTextCamera.WorldToViewportPoint(shootTextCreatPosition);//飘字初始锚点位置
            Vector2 changeAnchoredPosition = new Vector2((float)(anchors.x + shootTextComponent.xMoveOffeset), (float)(anchors.y + shootTextComponent.yMoveOffeset));//飘字这一帧所在位置

            //设定锚点
            shootTextComponent.rectTransform.anchorMax = anchors;
            shootTextComponent.rectTransform.anchorMin = anchors;
            //设置相对坐标
            shootTextComponent.rectTransform.anchoredPosition = changeAnchoredPosition;

            if (shootTextComponent.delayMoveTime <= Time.time)//允许移动操作
            {
                shootTextComponent.isMove = true;
            }

            //处理近大远小
            double objectHigh = ModelInScreenHigh(shootTextComponent.cacheTranform);
            double scale = (objectHigh / 100) * shootTextScaleFactor;

            shootTextComponent.ChangeScale(scale);
            double xMoveOffeset = horizontalMoveSpeed * deltaTime * objectHigh;
            double yMoveOffeset = verticalMoveSpeed * deltaTime * objectHigh;

            if (shootTextComponent.isMove == true)//处理位置信息
            {
                switch (shootTextComponent.moveType)
                {
                    case TextMoveType.None:
                        break;
                    case TextMoveType.Up:
                        {
                            shootTextComponent.yMoveOffeset += yMoveOffeset;
                        }
                        break;
                    case TextMoveType.Down:
                        {
                            shootTextComponent.yMoveOffeset -= yMoveOffeset;
                        }
                        break;
                    case TextMoveType.Left:
                        {
                            shootTextComponent.xMoveOffeset -= xMoveOffeset;
                        }
                        break;
                    case TextMoveType.Right:
                        {
                            shootTextComponent.xMoveOffeset += xMoveOffeset;
                        }
                        break;
                    case TextMoveType.LeftUp:
                        {
                            shootTextComponent.xMoveOffeset -= xMoveOffeset;
                            shootTextComponent.yMoveOffeset += yMoveOffeset;
                        }
                        break;
                    case TextMoveType.LeftDown:
                        {
                            shootTextComponent.xMoveOffeset -= xMoveOffeset;
                            shootTextComponent.yMoveOffeset -= yMoveOffeset;
                        }
                        break;
                    case TextMoveType.RightUp:
                        {
                            shootTextComponent.xMoveOffeset += xMoveOffeset;
                            shootTextComponent.yMoveOffeset += yMoveOffeset;
                        }
                        break;
                    case TextMoveType.RightDown:
                        {
                            shootTextComponent.xMoveOffeset += xMoveOffeset;
                            shootTextComponent.yMoveOffeset -= yMoveOffeset;

                        }
                        break;
                    case TextMoveType.LeftParabola:
                        {
                            float parabola = shootParabolaCure.Evaluate((float)(shootTextComponent.fadeCurveTime / moveLifeTime));
                            shootTextComponent.xMoveOffeset -= xMoveOffeset;
                            shootTextComponent.yMoveOffeset += yMoveOffeset + parabola;
                        }
                        break;
                    case TextMoveType.RightParabola:
                        {
                            float parabola = shootParabolaCure.Evaluate((float)(shootTextComponent.fadeCurveTime / moveLifeTime));
                            shootTextComponent.xMoveOffeset += xMoveOffeset;
                            shootTextComponent.yMoveOffeset += yMoveOffeset + parabola;
                        }
                        break;
                    default:
                        break;
                }
            }

            //处理渐隐
            if (shootTextComponent.isMove == true)
            {
                shootTextComponent.fadeCurveTime += deltaTime;
                float alpha = shootTextCure.Evaluate((float)(shootTextComponent.fadeCurveTime));
                shootTextComponent.ChangeAlpha(alpha);
            }
            else
            {
                shootTextComponent.ChangeAlpha(1);
            }

            //处理删除对应的飘字
            if (shootTextComponent.isMove == true && shootTextComponent.canvasGroup.alpha <= 0)
            {
                waitDestoryGroup.Add(shootTextComponent);
            }
        }

        //是否加速
        isAccelerate = waitShootTextGroup.Count >= accelerateThresholdValue ? true : false;
        if (isAccelerate)
        {
            updateCreatTime = updateCreatTime / accelerateFactor;
        }
        else
        {
            updateCreatTime = updateCreatDefualtTime;
        }

        //创建
        if ((updateCreatTempTime -= deltaTime) <= 0)
        {
            updateCreatTempTime = updateCreatTime;
            if (waitShootTextGroup.Count > 0)
            {
                GameObject tempObj = InstanceShootText(waitShootTextGroup.Dequeue());
                tempObj.transform.SetParent(ShootTextCanvas, false);
            }
        }

        //删除已经完全消失飘字
        for (int i = 0; i < waitDestoryGroup.Count; i++)
        {
            handleShootTextGroup.Remove(waitDestoryGroup[i]);
            Destroy(waitDestoryGroup[i].gameObject);
        }
        waitDestoryGroup.Clear();
    }
    public void CreatShootText(string content, Transform targetTranform)
    {
        CreatShootText(content, this.textAnimationType, this.textMoveType, this.delayMoveTime, this.initializedVerticalPositionOffset, this.initializedHorizontalPositionOffset, targetTranform);
    }
    public void CreatShootText(string content, TextAnimationType textAnimationType, TextMoveType textMoveType, float delayMoveTime, float initializedVerticalPositionOffset, float initializedHorizontalPositionOffset, Transform tempTransform)
    {
        ShootTextInfo shootTextInfo = new ShootTextInfo();
        shootTextInfo.content = content;
        shootTextInfo.animationType = textAnimationType;
        shootTextInfo.moveType = textMoveType;
        shootTextInfo.delayMoveTime = delayMoveTime;
        shootTextInfo.initializedVerticalPositionOffset = initializedVerticalPositionOffset;
        shootTextInfo.initializedHorizontalPositionOffset = initializedHorizontalPositionOffset;
        shootTextInfo.cacheTranform = tempTransform;

        CreatShootText(shootTextInfo);
    }
    public void CreatShootText(ShootTextInfo shootTextInfo)
    {
        if (CheckNumber(shootTextInfo.content))
        {
            if (IsAllowAddShootText())
            {
                waitShootTextGroup.Enqueue(shootTextInfo);
            }
            else
            {
                Debug.LogWarning("数量过多不能添加!!!");
            }
        }
        else
        {
            Debug.LogError("飘字数据不合法");
        }
    }

    private GameObject InstanceShootText(ShootTextInfo shootTextInfo)
    {
        GameObject shootText = Instantiate(shootTextPrefab);
        //先拼装字体，顺序颠倒会造成组件无法找到对应物体
        BuildNumber(shootTextInfo, shootText.transform);

        ShootTextComponent tempShootTextComponent = shootText.GetComponent<ShootTextComponent>();
        tempShootTextComponent.SetInfo(shootTextInfo);
        handleShootTextGroup.Add(tempShootTextComponent);
        return shootText;
    }

    private void BuildNumber(ShootTextInfo shootTextInfo, Transform parent)
    {
        string tempNumber = shootTextInfo.content;
        char numberOperator = tempNumber[0];
        string animationType = "";
        string plusOrMinus = "";

        //出现字体时对应的动画类型
        animationType = shootTextInfo.animationType == TextAnimationType.None ? TextAnimationType.Normal.ToString() : shootTextInfo.animationType.ToString();

        #region 运算符
        GameObject operatorObj = null;
        plusOrMinus = numberOperator == '+' ? operatorPlusKeyPostfix : operatorMinusKeyPostfix;

        operatorObj = Instantiate(numberDic[animationType + plusOrMinus]);
        operatorObj.transform.SetParent(parent, false);
        #endregion

        #region 数字
        GameObject numberObj = null;
        for (int i = 1; i < tempNumber.Length; i++)
        {
            numberObj = Instantiate(numberDic[animationType + numberPrefix + tempNumber[i]]);
            numberObj.transform.SetParent(parent, false);
        }
        #endregion
    }

    private double ModelInScreenHigh(Transform payerTranform)
    {
        Vector3 playerCenter = payerTranform.GetComponent<Collider>().bounds.center;
        Vector3 halfYVector3 = (Vector3.up * payerTranform.GetComponent<Collider>().bounds.size.y) * 0.5f;
        float topValue = ShootTextCamera.WorldToScreenPoint(playerCenter + halfYVector3).y;
        float bottomValue = ShootTextCamera.WorldToScreenPoint(playerCenter - halfYVector3).y;
        return topValue - bottomValue;
    }

    private bool IsAllowAddShootText()
    {
        return waitShootTextGroup.Count < MaxWaitCount;
    }
    /// <summary>
    /// 正则检查字符是否合法
    /// </summary>
    /// <param name="content">飘字内容</param>
    /// <returns></returns>
    private bool CheckNumber(string content)
    {
        string pattern = @"^(\+|\-)\d*$";
        bool IsLegal = Regex.IsMatch(content, pattern);
        return IsLegal;
    }
}

