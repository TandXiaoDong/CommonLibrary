using ZedGraph;
using System.Drawing;

namespace EquipmentClient.ControlGraph
{
    class ZedControlGraphMonitor
    {
        ///选择的图形类型：波形图  、 特征值图
        ///选择X/Y/Z分量，可同时选择所有，且选择的分量都占用整个panel
        ///显示方式：分开与合并
        ///排列方式：横向、纵向
        ///背景：背景颜色
        ///线条：类型、颜色、大小
        ///网格：是否显示、颜色
        ///坐标轴：自适应、最大值、最小值、等分(纵轴正数或负数部分的等分，控制步长即可)
        ///特征值图特有属性：选择10个特征值显示（显示处可调整为选择唯一或多个）
        ///
        //#region graph params

        //public static bool IsGraph_x;
        //public static bool IsGraph_y;
        //public static bool IsGraph_z;
        ///// <summary>
        ///// true-div;false-union
        ///// </summary>
        //public static bool IsGraphDivOrUnion;
        ///// <summary>
        ///// true-level;false-vertical
        ///// </summary>
        //public static bool IsGraphLevelOrVertical;
        ///// <summary>
        ///// 背景颜色
        ///// </summary>
        //public static Color zedControlColor;
        ///// <summary>
        ///// 字体颜色，字体颜色与背景颜色不能一致
        ///// </summary>
        //public static Color graphFontColor;
        ///// <summary>
        ///// 网格颜色
        ///// </summary>
        //public static Color gridLineColor;
        ///// <summary>
        ///// 显示网格线
        ///// </summary>
        //public static bool IsGridLine;
        ///// <summary>
        ///// 线条大小
        ///// </summary>
        //public static float lineSize;
        ///// <summary>
        ///// 线条类型
        ///// </summary>
        //public static AxisType LineAxisType;
        ///// <summary>
        ///// 线条颜色
        ///// </summary>
        //public static Color LineColor;
        ///// <summary>
        ///// 曲线符号类型
        ///// </summary>
        //public static SymbolType symbolType;
        ///// <summary>
        ///// 是否填充，true-填充，false-不填充
        ///// </summary>
        //public static bool IsLineFill;
        ///// <summary>
        ///// 是否显示
        ///// </summary>
        //public static bool IsLineVisiable;
        ///// <summary>
        ///// X自适应，true-X自适应，false-X设置最大值
        ///// </summary>
        //public static bool IsXAdapter;
        //public static double XMax;
        ///// <summary>
        ///// true-Y自适应，false-Y设置最大值
        ///// </summary>
        //public static bool IsYAdapter;
        //public static double YMax;
        //public static int YAveNum;
        ///// <summary>
        ///// 当前图形的宽度
        ///// </summary>
        //private int graphWidth;
        ///// <summary>
        ///// 当前图形的高度
        ///// </summary>
        //private int graphHeight;

        //private static float axisFontSize;
        //private static float titleFontSize;

        //private ZedGraphControl zedCt1, zedCt2, zedCt3;

        //private static GraphPane myPane1, myPane2, myPane3;

        //private static LineItem lineItem1, lineItem2, lineItem3;

        //private static string ZED_TITLE1 = "X原始信号";
        //private static string ZED_TITLE2 = "Y原始信号";
        //private static string ZED_TITLE3 = "Z原始信号";

        //public enum GraphType
        //{
        //    WAVE_GRAPH,
        //    CHARACT_GRAPH
        //}

        //#endregion

        //public void InitDefaultGraph()
        //{
        //    IsGraph_x = ComGraph.GraphConfig.IsGraphX;
        //    IsGraph_y = ComGraph.GraphConfig.IsGraphY;
        //    IsGraph_z = ComGraph.GraphConfig.IsGraphZ;
        //    ZED_TITLE1 = ComGraph.GraphConfig.ZED_TITLE1;
        //    ZED_TITLE2 = ComGraph.GraphConfig.ZED_TITLE2;
        //    ZED_TITLE3 = ComGraph.GraphConfig.ZED_TITLE3;
        //    zedControlColor = ComGraph.GraphConfig.zedControlColor;
        //    graphFontColor = ComGraph.GraphConfig.graphFontColor;
        //    LineAxisType = ComGraph.GraphConfig.LineAxisType;
        //    LineColor = ComGraph.GraphConfig.LineColor;
        //    gridLineColor = ComGraph.GraphConfig.gridLineColor;
        //    IsGridLine = ComGraph.GraphConfig.IsGridLine;
        //    lineSize = ComGraph.GraphConfig.lineSize;
        //    IsGraphDivOrUnion = ComGraph.GraphConfig.IsGraphDivOrUnion;
        //    IsGraphLevelOrVertical = ComGraph.GraphConfig.IsGraphLevelOrVertical;
        //    symbolType = ComGraph.GraphConfig.symbolType;
        //    IsLineFill = ComGraph.GraphConfig.IsLineFill;
        //    IsXAdapter = ComGraph.GraphConfig.IsXAdapter;
        //    IsYAdapter = ComGraph.GraphConfig.IsYAdapter;
        //    XMax = ComGraph.GraphConfig.XMax;
        //    YMax = ComGraph.GraphConfig.YMax;
        //    YAveNum = ComGraph.GraphConfig.YAveNum;
        //    titleFontSize = ComGraph.GraphConfig.titleFontSize;
        //    axisFontSize = ComGraph.GraphConfig.axisFontSize;
        //}

        //public ZedControlGraphMonitor(ZedControls.ZedControl zedContrl, ZedControls.ZedPointPairlist pplist, PanelEx panelEx1)
        //{
        //    panelEx1.Controls.Clear();

        //    #region new zedControl
        //    zedContrl.ZedControl1 = new ZedGraphControl();
        //    zedContrl.ZedControl2 = new ZedGraphControl();
        //    zedContrl.ZedControl3 = new ZedGraphControl();
        //    #endregion

        //    #region new pplist
        //    if (pplist.PPlist1 == null)
        //        pplist.PPlist1 = new PointPairList();
        //    if (pplist.PPlist2 == null)
        //        pplist.PPlist2 = new PointPairList();
        //    if (pplist.PPlist3 == null)
        //        pplist.PPlist3 = new PointPairList();
        //    #endregion

        //    this.zedCt1 = zedContrl.ZedControl1;
        //    this.zedCt2 = zedContrl.ZedControl2;
        //    this.zedCt3 = zedContrl.ZedControl3;
        //}

        //public void UsedProperty()
        //{
        //    //graph properties

        //    #region set mouse level and vertical move
        //    if (zedCt1 != null)
        //    {
        //        zedCt1.IsEnableHPan = false;
        //        zedCt1.IsEnableVPan = false;
        //    }
        //    if (zedCt2 != null)
        //    {
        //        zedCt2.IsEnableHPan = false;
        //        zedCt2.IsEnableVPan = false;
        //    }
        //    if (zedCt3 != null)
        //    {
        //        zedCt3.IsEnableHPan = false;
        //        zedCt3.IsEnableVPan = false;
        //    }
        //    #endregion

        //    #region 设置鼠标拖动

        //    if (zedCt1 != null)
        //        zedCt1.PanModifierKeys = Keys.None;
        //    if (zedCt2 != null)
        //        zedCt2.PanModifierKeys = Keys.None;
        //    if (zedCt3 != null)
        //        zedCt3.PanModifierKeys = Keys.None;
        //    #endregion

        //    #region 横向缩放/纵向缩放

        //    if (zedCt1 != null)
        //        zedCt1.IsEnableHZoom = false;
        //    if (zedCt2 != null)
        //        zedCt2.IsEnableHZoom = false;
        //    if (zedCt3 != null)
        //        zedCt3.IsEnableHZoom = false;
        //    //纵向缩放;
        //    if (zedCt1 != null)
        //        zedCt1.IsEnableVZoom = false;
        //    if (zedCt2 != null)
        //        zedCt2.IsEnableVZoom = false;
        //    if (zedCt3 != null)
        //        zedCt3.IsEnableVZoom = false;

        //    ///鼠标横向拖动

        //    if (zedCt1 != null)
        //        zedCt1.IsEnableHPan = false;
        //    if (zedCt2 != null)
        //        zedCt2.IsEnableHPan = false;
        //    if (zedCt3 != null)
        //        zedCt3.IsEnableHPan = false;

        //    ///鼠标纵向拖动
        //    ///
        //    if (zedCt1 != null)
        //        zedCt1.IsEnableVPan = false;
        //    if (zedCt2 != null)
        //        zedCt2.IsEnableVPan = false;
        //    if (zedCt3 != null)
        //        zedCt3.IsEnableVPan = false;
        //    #endregion

        //    #region show point
        //    if (zedCt1 != null)
        //        zedCt1.IsShowPointValues = ComGraph.GraphConfig.bIsShowPoint;
        //    if (zedCt2 != null)
        //        zedCt2.IsShowPointValues = ComGraph.GraphConfig.bIsShowPoint;
        //    if (zedCt3 != null)
        //        zedCt3.IsShowPointValues = ComGraph.GraphConfig.bIsShowPoint;

        //    #endregion

        //    #region init graphPane
        //    myPane1 = new GraphPane();
        //    myPane2 = new GraphPane();
        //    myPane3 = new GraphPane();

        //    if (zedCt1 != null)
        //        myPane1 = zedCt1.GraphPane;
        //    if (zedCt2 != null)
        //        myPane2 = zedCt2.GraphPane;
        //    if (zedCt3 != null)
        //        myPane3 = zedCt3.GraphPane;
        //    #endregion

        //    #region set myPane's properties: xaxis's type 
        //    myPane1.XAxis.Type = LineAxisType;
        //    myPane2.XAxis.Type = LineAxisType;
        //    myPane3.XAxis.Type = LineAxisType;
        //    #endregion

        //    #region 标题设置
        //    myPane1.Title.Text = ZED_TITLE1;
        //    myPane2.Title.Text = ZED_TITLE2;
        //    myPane3.Title.Text = ZED_TITLE3;

        //    ///x
        //    myPane1.XAxis.Title.Text = "";
        //    myPane2.XAxis.Title.Text = "";
        //    myPane3.XAxis.Title.Text = "";
        //    ///y
        //    myPane1.YAxis.Title.Text = "";
        //    myPane2.YAxis.Title.Text = "";
        //    myPane3.YAxis.Title.Text = "";
        //    #endregion

        //    #region show right context menu

        //    if (zedCt1 != null)
        //        zedCt1.IsShowContextMenu = true;
        //    if (zedCt2 != null)
        //        zedCt2.IsShowContextMenu = true;
        //    if (zedCt3 != null)
        //        zedCt3.IsShowContextMenu = true;
        //    #endregion


        //    //曲线标签位置 
        //    //zedCt1.GraphPane.Legend.Position = LegendPos.Top;
        //    //zedCt1.GraphPane.Legend.IsShowLegendSymbols = false;
        //    //zedCt1.GraphPane.Legend.IsReverse = false;
        //    //zedCt1.GraphPane.Legend.IsHStack = false;
        //    //zedCt1.GraphPane.Legend.IsVisible = true;
        //    //zedCt1.GraphPane.XAxis.Title.IsOmitMag = true;

        //    GraphBackColor();
        //    TitleFontSpec();
        //    TitleFontColor(graphFontColor);

        //    AxisFontSpec();
        //    AxisFontColor();
        //    AxisScalColor();

        //    AxisColor();
        //    GraphLegend();

        //    ///边框
        //    ChartBorder(false, BorderStyle.None);
        //    AxisFontSize(axisFontSize);
        //    TitleFontSize();
        //    LegendFontSize();
        //}

        //#region UsedProperty
        //private void GraphBackColor()
        //{
        //    //背景透明
        //    myPane1.Chart.Fill = new Fill(zedControlColor, zedControlColor, 45.0f);
        //    myPane1.Fill.Color = zedControlColor;

        //    myPane2.Chart.Fill = new Fill(zedControlColor, zedControlColor, 45.0f);
        //    myPane2.Fill.Color = zedControlColor;

        //    myPane3.Chart.Fill = new Fill(zedControlColor, zedControlColor, 45.0f);
        //    myPane3.Fill.Color = zedControlColor;
        //}

        //private void GraphLegend()
        //{
        //    //曲线标签位置 
        //    myPane1.Legend.Fill.Brush = Brushes.Transparent;
        //    myPane1.Legend.FontSpec.FontColor = ComGraph.GraphConfig.graphFontColor;

        //    myPane2.Legend.Fill.Brush = Brushes.Transparent;
        //    myPane2.Legend.FontSpec.FontColor = ComGraph.GraphConfig.graphFontColor;

        //    myPane3.Legend.Fill.Brush = Brushes.Transparent;
        //    myPane3.Legend.FontSpec.FontColor = ComGraph.GraphConfig.graphFontColor;
        //}

        //private void TitleFontSpec()
        //{
        //    myPane1.Title.FontSpec.IsBold = ComGraph.GraphConfig.IsBold;
        //    myPane1.Title.FontSpec.IsAntiAlias = ComGraph.GraphConfig.IsAntiAlias;
        //    myPane1.Title.FontSpec.IsItalic = ComGraph.GraphConfig.IsItalic;

        //    myPane2.Title.FontSpec.IsBold = ComGraph.GraphConfig.IsBold;
        //    myPane2.Title.FontSpec.IsAntiAlias = ComGraph.GraphConfig.IsAntiAlias;
        //    myPane2.Title.FontSpec.IsItalic = ComGraph.GraphConfig.IsItalic;

        //    myPane3.Title.FontSpec.IsBold = ComGraph.GraphConfig.IsBold;
        //    myPane3.Title.FontSpec.IsAntiAlias = ComGraph.GraphConfig.IsAntiAlias;
        //    myPane3.Title.FontSpec.IsItalic = ComGraph.GraphConfig.IsItalic;
        //}

        //public void TitleFontColor(Color tfColor)
        //{
        //    myPane1.Title.FontSpec.FontColor = tfColor;
        //    myPane2.Title.FontSpec.FontColor = tfColor;
        //    myPane3.Title.FontSpec.FontColor = tfColor;
        //}

        //public void TitleFontSize()
        //{
        //    myPane1.Title.FontSpec.Size = ComGraph.GraphConfig.titleFontSize;
        //    myPane2.Title.FontSpec.Size = ComGraph.GraphConfig.titleFontSize;
        //    myPane3.Title.FontSpec.Size = ComGraph.GraphConfig.titleFontSize;
        //}

        //public void LegendFontSize()
        //{
        //    myPane1.Legend.FontSpec.Size = ComGraph.GraphConfig.titleFontSize;
        //    myPane2.Legend.FontSpec.Size = ComGraph.GraphConfig.titleFontSize;
        //    myPane3.Legend.FontSpec.Size = ComGraph.GraphConfig.titleFontSize;
        //}

        //private void AxisFontSpec()
        //{
        //    ///x 粗体、反、斜体
        //    myPane1.XAxis.Scale.FontSpec.IsBold = ComGraph.GraphConfig.IsBold;
        //    myPane1.XAxis.Scale.FontSpec.IsAntiAlias = ComGraph.GraphConfig.IsAntiAlias;
        //    myPane1.XAxis.Scale.FontSpec.IsItalic = ComGraph.GraphConfig.IsItalic;

        //    myPane2.XAxis.Scale.FontSpec.IsBold = ComGraph.GraphConfig.IsBold;
        //    myPane2.XAxis.Scale.FontSpec.IsAntiAlias = ComGraph.GraphConfig.IsAntiAlias;
        //    myPane2.XAxis.Scale.FontSpec.IsItalic = ComGraph.GraphConfig.IsItalic;

        //    myPane3.XAxis.Scale.FontSpec.IsBold = ComGraph.GraphConfig.IsBold;
        //    myPane3.XAxis.Scale.FontSpec.IsAntiAlias = ComGraph.GraphConfig.IsAntiAlias;
        //    myPane3.XAxis.Scale.FontSpec.IsItalic = ComGraph.GraphConfig.IsItalic;

        //    ///y
        //    myPane1.YAxis.Scale.FontSpec.IsBold = ComGraph.GraphConfig.IsBold;
        //    myPane1.YAxis.Scale.FontSpec.IsAntiAlias = ComGraph.GraphConfig.IsAntiAlias;
        //    myPane1.YAxis.Scale.FontSpec.IsItalic = ComGraph.GraphConfig.IsItalic;

        //    myPane2.YAxis.Scale.FontSpec.IsBold = ComGraph.GraphConfig.IsBold;
        //    myPane2.YAxis.Scale.FontSpec.IsAntiAlias = ComGraph.GraphConfig.IsAntiAlias;
        //    myPane2.YAxis.Scale.FontSpec.IsItalic = ComGraph.GraphConfig.IsItalic;

        //    myPane3.YAxis.Scale.FontSpec.IsBold = ComGraph.GraphConfig.IsBold;
        //    myPane3.YAxis.Scale.FontSpec.IsAntiAlias = ComGraph.GraphConfig.IsAntiAlias;
        //    myPane3.YAxis.Scale.FontSpec.IsItalic = ComGraph.GraphConfig.IsItalic;
        //}

        //private void AxisFontColor()
        //{
        //    myPane1.XAxis.Scale.FontSpec.FontColor = ComGraph.GraphConfig.graphFontColor;
        //    myPane2.XAxis.Scale.FontSpec.FontColor = ComGraph.GraphConfig.graphFontColor;
        //    myPane3.XAxis.Scale.FontSpec.FontColor = ComGraph.GraphConfig.graphFontColor;

        //    myPane1.YAxis.Scale.FontSpec.FontColor = ComGraph.GraphConfig.graphFontColor;
        //    myPane2.YAxis.Scale.FontSpec.FontColor = ComGraph.GraphConfig.graphFontColor;
        //    myPane3.YAxis.Scale.FontSpec.FontColor = ComGraph.GraphConfig.graphFontColor;
        //}

        //public void AxisFontSize(float xyFontSize)
        //{
        //    myPane1.XAxis.Scale.FontSpec.Size = xyFontSize;
        //    myPane2.XAxis.Scale.FontSpec.Size = xyFontSize;
        //    myPane3.XAxis.Scale.FontSpec.Size = xyFontSize;

        //    myPane1.YAxis.Scale.FontSpec.Size = xyFontSize;
        //    myPane2.YAxis.Scale.FontSpec.Size = xyFontSize;
        //    myPane3.YAxis.Scale.FontSpec.Size = xyFontSize;
        //}

        //private void AxisScalColor()
        //{
        //    myPane1.XAxis.MajorTic.Color = ComGraph.GraphConfig.graphBorderColor;
        //    myPane2.XAxis.MajorTic.Color = ComGraph.GraphConfig.graphBorderColor;
        //    myPane3.XAxis.MajorTic.Color = ComGraph.GraphConfig.graphBorderColor;

        //    myPane1.YAxis.MajorTic.Color = ComGraph.GraphConfig.graphBorderColor;
        //    myPane2.YAxis.MajorTic.Color = ComGraph.GraphConfig.graphBorderColor;
        //    myPane3.YAxis.MajorTic.Color = ComGraph.GraphConfig.graphBorderColor;
        //}

        //private void AxisColor()
        //{
        //    ///中心轴颜色
        //    myPane1.YAxis.Color = ComGraph.GraphConfig.graphBorderColor;
        //    myPane2.YAxis.Color = ComGraph.GraphConfig.graphBorderColor;
        //    myPane3.YAxis.Color = ComGraph.GraphConfig.graphBorderColor;
        //}

        //private void ChartBorder(bool isVisible, BorderStyle style)
        //{
        //    //图表边框颜色： 
        //    //内边框 
        //    myPane1.Chart.Border.Color = ComGraph.GraphConfig.graphBorderColor;
        //    myPane2.Chart.Border.Color = ComGraph.GraphConfig.graphBorderColor;
        //    myPane3.Chart.Border.Color = ComGraph.GraphConfig.graphBorderColor;
        //    //外边框 
        //    myPane1.Border = new Border(ComGraph.GraphConfig.graphBorderColor, 0);
        //    myPane2.Border = new Border(ComGraph.GraphConfig.graphBorderColor, 0);
        //    myPane3.Border = new Border(ComGraph.GraphConfig.graphBorderColor, 0);

        //    //隐藏外边框： 
        //    //zedCt1.BorderStyle = style;
        //    zedCt1.GraphPane.Border.IsVisible = isVisible;
        //    zedCt2.GraphPane.Border.IsVisible = isVisible;
        //    zedCt3.GraphPane.Border.IsVisible = isVisible;
        //}
        //#endregion

        ///// <summary>
        ///// 坐标轴刻度设置 、自适应
        ///// </summary>
        ///// <param name="zedCtl"></param>
        //public void GraphAxis()
        //{
        //    if (IsXAdapter)
        //    {
        //        AxisXadapter();
        //    }
        //    else
        //    {
        //        AxisXvalue();
        //        AxisYvalue();
        //    }

        //    if (IsYAdapter)
        //    {
        //        AxisYadapter();
        //    }
        //    else
        //    {
        //        AxisXvalue();
        //        AxisYvalue();
        //    }
        //}

        //#region Axis set
        //private void AxisXadapter()
        //{
        //    myPane1.XAxis.Scale.MaxAuto = IsXAdapter;
        //    myPane2.XAxis.Scale.MaxAuto = IsXAdapter;
        //    myPane3.XAxis.Scale.MaxAuto = IsXAdapter;
        //}

        //private void AxisYadapter()
        //{
        //    myPane1.YAxis.Scale.MaxAuto = IsYAdapter;
        //    myPane2.YAxis.Scale.MaxAuto = IsYAdapter;
        //    myPane3.YAxis.Scale.MaxAuto = IsYAdapter;
        //}

        //private static void AxisXvalue()
        //{
        //    AxisX1Value();
        //    AxisX2Value();
        //    AxisX3Value();
        //}

        //private static void AxisYvalue()
        //{
        //    AxisY1Value();
        //    AxisY2Value();
        //    AxisY3Value();
        //}

        //#region AxisX value one to eight
        //private static void AxisX1Value()
        //{
        //    myPane1.XAxis.Scale.Max = XMax;
        //    myPane1.XAxis.Scale.MajorStep = XMax / (YAveNum * 5);
        //    myPane1.XAxis.Scale.MinorStep = XMax;
        //    myPane1.XAxis.Scale.Min = 0;
        //}

        //private static void AxisX2Value()
        //{
        //    myPane2.XAxis.Scale.Max = XMax;
        //    myPane2.XAxis.Scale.MajorStep = XMax / (YAveNum * 5);
        //    myPane2.XAxis.Scale.MinorStep = XMax;
        //    myPane2.XAxis.Scale.Min = 0;
        //}

        //private static void AxisX3Value()
        //{
        //    myPane3.XAxis.Scale.Max = XMax;
        //    myPane3.XAxis.Scale.MajorStep = XMax / (YAveNum * 5);
        //    myPane3.XAxis.Scale.MinorStep = XMax;
        //    myPane3.XAxis.Scale.Min = 0;
        //}
        //#endregion

        //#region AxisY value one to eight
        //private static void AxisY1Value()
        //{
        //    myPane1.YAxis.Scale.Max = YMax;
        //    myPane1.YAxis.Scale.MajorStep = YMax / YAveNum;
        //    myPane1.YAxis.Scale.MinorStep = YMax;
        //    myPane1.YAxis.Scale.Min = -YMax;
        //}

        //private static void AxisY2Value()
        //{
        //    myPane2.YAxis.Scale.Max = YMax;
        //    myPane2.YAxis.Scale.MajorStep = YMax / YAveNum;
        //    myPane2.YAxis.Scale.MinorStep = YMax;
        //    myPane2.YAxis.Scale.Min = -YMax;
        //}

        //private static void AxisY3Value()
        //{
        //    myPane3.YAxis.Scale.Max = YMax;
        //    myPane3.YAxis.Scale.MajorStep = YMax / YAveNum;
        //    myPane3.YAxis.Scale.MinorStep = YMax;
        //    myPane3.YAxis.Scale.Min = -YMax;
        //}
        //#endregion

        //#endregion

        //public void GraphGrid()
        //{
        //    ///图形显示网格、大小、颜色
        //    GraphGridVisible();

        //    GridColor();
        //}

        //#region GraphGridLine
        //private void GraphGridVisible()
        //{
        //    myPane1.XAxis.MajorGrid.IsVisible = IsGridLine;
        //    myPane1.YAxis.MajorGrid.IsVisible = IsGridLine;

        //    myPane2.XAxis.MajorGrid.IsVisible = IsGridLine;
        //    myPane2.YAxis.MajorGrid.IsVisible = IsGridLine;

        //    myPane3.XAxis.MajorGrid.IsVisible = IsGridLine;
        //    myPane3.YAxis.MajorGrid.IsVisible = IsGridLine;
        //}

        //private void GridColor()
        //{
        //    myPane1.XAxis.MajorGrid.Color = gridLineColor;
        //    myPane1.YAxis.MajorGrid.Color = gridLineColor;

        //    myPane2.XAxis.MajorGrid.Color = gridLineColor;
        //    myPane2.YAxis.MajorGrid.Color = gridLineColor;

        //    myPane3.XAxis.MajorGrid.Color = gridLineColor;
        //    myPane3.YAxis.MajorGrid.Color = gridLineColor;
        //}
        //#endregion

        //public void AddZedControl(PanelEx panelEx1, ZedControls.ZedPointPairlist pplist, ZedControls.ZedAnnotation annota,DotControls dotControl)
        //{
        //    #region
        //    panelEx1.Controls.Clear();
        //    ///3*3 - 1
        //    if (IsGraph_x && !IsGraph_y && !IsGraph_z)
        //    {
        //        ///x
        //        ///水平与垂直，波形图与特征值图同时选择合并时
        //        ///
        //        if (IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //        {
        //            graphWidth = panelEx1.Width / 2;
        //            graphHeight = panelEx1.Height;
        //        }
        //        else if (!IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //        {
        //            graphWidth = panelEx1.Width / 2;
        //            graphHeight = panelEx1.Height;
        //        }
        //        else
        //        {
        //            graphWidth = panelEx1.Width;
        //            graphHeight = panelEx1.Height;
        //        }
        //        AddZedControlSignalGraph(panelEx1, dotControl,zedCt1);
        //        LineItemPropertySignalLine(annota.Annotation1,pplist.PPlist1,lineItem1,myPane1);
        //    }
        //    else if (!IsGraph_x && IsGraph_y && !IsGraph_z)
        //    {
        //        ///y
        //        ///
        //        if (IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //        {
        //            graphWidth = panelEx1.Width / 2;
        //            graphHeight = panelEx1.Height;
        //        }
        //        else if (!IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //        {
        //            graphWidth = panelEx1.Width / 2;
        //            graphHeight = panelEx1.Height;
        //        }
        //        else
        //        {
        //            graphWidth = panelEx1.Width;
        //            graphHeight = panelEx1.Height;
        //        }
        //        AddZedControlSignalGraph(panelEx1, dotControl, zedCt2);
        //        LineItemPropertySignalLine(annota.Annotation2, pplist.PPlist2, lineItem2, myPane2);
        //    }
        //    else if (!IsGraph_x && !IsGraph_y && IsGraph_z)
        //    {
        //        ///z
        //        ///
        //        if (IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //        {
        //            graphWidth = panelEx1.Width / 2;
        //            graphHeight = panelEx1.Height;
        //        }
        //        else if (!IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //        {
        //            graphWidth = panelEx1.Width / 2;
        //            graphHeight = panelEx1.Height;
        //        }
        //        else
        //        {
        //            graphWidth = panelEx1.Width;
        //            graphHeight = panelEx1.Height;
        //        }
        //        AddZedControlSignalGraph(panelEx1, dotControl, zedCt3);
        //        LineItemPropertySignalLine(annota.Annotation3, pplist.PPlist3, lineItem3, myPane3);
        //    }
        //    else if (IsGraph_x && IsGraph_y && !IsGraph_z)
        //    {
        //        ///xy
        //        ///分开与合并
        //        ///
        //        if (IsGraphDivOrUnion)
        //        {
        //            ///分开
        //            ///特征值同时也水平
        //            if (IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                graphWidth = panelEx1.Width / 2;
        //                graphHeight = panelEx1.Height / 2;
        //            }
        //            else
        //            {
        //                if (IsGraphLevelOrVertical && !ComGraph.GraphTvConfig.IsVisable)
        //                {
        //                    graphWidth = panelEx1.Width / 2;
        //                    graphHeight = panelEx1.Height;
        //                }
        //                else if (!IsGraphLevelOrVertical && !ComGraph.GraphTvConfig.IsVisable)
        //                {
        //                    graphWidth = panelEx1.Width;
        //                    graphHeight = panelEx1.Height / 2;
        //                }
        //                else if (!IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //                {
        //                    graphWidth = panelEx1.Width / 2;
        //                    graphHeight = panelEx1.Height / 2;
        //                }
        //            }
        //            AddZedControlTwoGraph(panelEx1, dotControl, zedCt1,zedCt2);
        //            LineItemPropertySignalLine(annota.Annotation1, pplist.PPlist1, lineItem1, myPane1);
        //            LineItemPropertySignalLine(annota.Annotation2, pplist.PPlist2, lineItem2, myPane2);
        //        }
        //        else
        //        {
        //            ///xy合并显示
        //            ///特征值同时也水平
        //            if (IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                graphWidth = panelEx1.Width / 2;
        //                graphHeight = panelEx1.Height;
        //            }
        //            else if (!IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                graphWidth = panelEx1.Width / 2;
        //                graphHeight = panelEx1.Height;
        //            }
        //            else
        //            {
        //                graphWidth = panelEx1.Width;
        //                graphHeight = panelEx1.Height;
        //            }
        //            AddZedControlTwoGraph(panelEx1, dotControl, zedCt1, zedCt2);
        //            LineItemPropertyTwoLine(annota.Annotation1,annota.Annotation2,pplist.PPlist1,pplist.PPlist2,lineItem1,lineItem2,myPane1,myPane2);
        //        }
        //    }
        //    else if (IsGraph_x && !IsGraph_y && IsGraph_z)
        //    {
        //        ///xz
        //        ///分开与合并
        //        if (IsGraphDivOrUnion)
        //        {
        //            ///分开
        //            ///
        //            if (IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                graphWidth = panelEx1.Width / 2;
        //                graphHeight = panelEx1.Height / 2;
        //            }
        //            else
        //            {
        //                if (IsGraphLevelOrVertical && !ComGraph.GraphTvConfig.IsVisable)
        //                {
        //                    graphWidth = panelEx1.Width / 2;
        //                    graphHeight = panelEx1.Height;
        //                }
        //                else if (!IsGraphLevelOrVertical && !ComGraph.GraphTvConfig.IsVisable)
        //                {
        //                    graphWidth = panelEx1.Width;
        //                    graphHeight = panelEx1.Height / 2;
        //                }
        //                else if (!IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //                {
        //                    graphWidth = panelEx1.Width / 2;
        //                    graphHeight = panelEx1.Height / 2;
        //                }
        //            }
        //            AddZedControlTwoGraph(panelEx1, dotControl, zedCt1,zedCt3);
        //            LineItemPropertySignalLine(annota.Annotation1, pplist.PPlist1, lineItem1, myPane1);
        //            LineItemPropertySignalLine(annota.Annotation3, pplist.PPlist3, lineItem3, myPane3);
        //        }
        //        else
        //        {
        //            ///合并
        //            ///
        //            if (IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                graphWidth = panelEx1.Width / 2;
        //                graphHeight = panelEx1.Height;
        //            }
        //            else if (!IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                graphWidth = panelEx1.Width / 2;
        //                graphHeight = panelEx1.Height;
        //            }
        //            else
        //            {
        //                graphWidth = panelEx1.Width;
        //                graphHeight = panelEx1.Height;
        //            }
        //            AddZedControlTwoGraph(panelEx1, dotControl, zedCt1,zedCt2);
        //            LineItemPropertyTwoLine(annota.Annotation1, annota.Annotation3, pplist.PPlist1, pplist.PPlist3, lineItem1, lineItem3, myPane1, myPane3);
        //        }
        //    }
        //    else if (!IsGraph_x && IsGraph_y && IsGraph_z)
        //    {
        //        ///yz
        //        ///分开与合并
        //        ///
        //        if (IsGraphDivOrUnion)
        //        {
        //            ///分开
        //            ///
        //            if (IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                graphWidth = panelEx1.Width / 2;
        //                graphHeight = panelEx1.Height / 2;
        //            }
        //            else
        //            {
        //                if (IsGraphLevelOrVertical && !ComGraph.GraphTvConfig.IsVisable)
        //                {
        //                    graphWidth = panelEx1.Width / 2;
        //                    graphHeight = panelEx1.Height;
        //                }
        //                else if (!IsGraphLevelOrVertical && !ComGraph.GraphTvConfig.IsVisable)
        //                {
        //                    graphWidth = panelEx1.Width;
        //                    graphHeight = panelEx1.Height / 2;
        //                }
        //                else if (!IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //                {
        //                    graphWidth = panelEx1.Width / 2;
        //                    graphHeight = panelEx1.Height / 2;
        //                }
        //            }
        //            AddZedControlTwoGraph(panelEx1, dotControl, zedCt2,zedCt3);
        //            LineItemPropertySignalLine(annota.Annotation2, pplist.PPlist2, lineItem2, myPane2);
        //            LineItemPropertySignalLine(annota.Annotation3, pplist.PPlist3, lineItem3, myPane3);
        //        }
        //        else
        //        {
        //            ///合并
        //            ///
        //            if (IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                graphWidth = panelEx1.Width / 2;
        //                graphHeight = panelEx1.Height;
        //            }
        //            else if (!IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                graphWidth = panelEx1.Width / 2;
        //                graphHeight = panelEx1.Height;
        //            }
        //            else
        //            {
        //                graphWidth = panelEx1.Width;
        //                graphHeight = panelEx1.Height;
        //            }
        //            AddZedControlTwoGraph(panelEx1, dotControl, zedCt2, zedCt3);
        //            LineItemPropertyTwoLine(annota.Annotation2, annota.Annotation3, pplist.PPlist2, pplist.PPlist3, lineItem2, lineItem3, myPane2, myPane3);
        //        }
        //    }
        //    else if (IsGraph_x && IsGraph_y && IsGraph_z)
        //    {
        //        ///xyz
        //        ///分开与合并
        //        ///
        //        if (IsGraphDivOrUnion)
        //        {
        //            ///分开
        //            ///
        //            if (IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                graphWidth = panelEx1.Width / 2;
        //                graphHeight = panelEx1.Height / 3;
        //            }
        //            else
        //            {
        //                if (IsGraphLevelOrVertical && !ComGraph.GraphTvConfig.IsVisable)
        //                {
        //                    graphWidth = panelEx1.Width / 3;
        //                    graphHeight = panelEx1.Height;
        //                }
        //                else if (!IsGraphLevelOrVertical && !ComGraph.GraphTvConfig.IsVisable)
        //                {
        //                    graphWidth = panelEx1.Width;
        //                    graphHeight = panelEx1.Height / 3;
        //                }
        //                else if (!IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //                {
        //                    graphWidth = panelEx1.Width / 2;
        //                    graphHeight = panelEx1.Height / 3; 
        //                }
        //            }
        //            AddZedControlThreeGraph(panelEx1, dotControl, zedCt1,zedCt2,zedCt3);
        //            LineItemPropertySignalLine(annota.Annotation1, pplist.PPlist1, lineItem1, myPane1);
        //            LineItemPropertySignalLine(annota.Annotation2, pplist.PPlist2, lineItem2, myPane2);
        //            LineItemPropertySignalLine(annota.Annotation3, pplist.PPlist3, lineItem3, myPane3);
        //        }
        //        else
        //        {
        //            ///合并
        //            ///
        //            if (IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                graphWidth = panelEx1.Width / 2;
        //                graphHeight = panelEx1.Height;
        //            }
        //            else if (!IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                graphWidth = panelEx1.Width / 2;
        //                graphHeight = panelEx1.Height;
        //            }
        //            else
        //            {
        //                graphWidth = panelEx1.Width;
        //                graphHeight = panelEx1.Height;
        //            }
        //            AddZedControlSignalGraph(panelEx1, dotControl, zedCt1);
        //            LineItemPropertyThreeLine(annota.Annotation1,annota.Annotation2, annota.Annotation3,pplist.PPlist1, 
        //                pplist.PPlist2, pplist.PPlist3, lineItem1,lineItem2, lineItem3, myPane1,myPane2, myPane3);
        //        }
        //    }
        //    else
        //    {
        //        ///!x!y!z
        //        ///
                
        //    }
        //    #endregion
        //}

        //private void AddZedControlSignalGraph(PanelEx panelEx1, DotControls dotControl, ZedGraphControl zedControl)
        //{
        //    if (!panelEx1.Contains(zedControl))
        //        panelEx1.Controls.Add(zedControl);

        //    //zedCt1.Dock = DockStyle.Fill;
        //    zedControl.Size = new Size(graphWidth, graphHeight);

        //    #region add annotion control
        //    //if (!zedControl.Contains(dotControl.XPointLeft))
        //    //    zedControl.Controls.Add(dotControl.XPointLeft);
        //    //if (!zedControl.Contains(dotControl.XPointRange))
        //    //    zedControl.Controls.Add(dotControl.XPointRange);
        //    //if (!zedControl.Contains(dotControl.XPointRight))
        //    //    zedControl.Controls.Add(dotControl.XPointRight);

        //    //dotControl.XPointLeft.BackColor = zedControlColor;
        //    //dotControl.XPointRight.BackColor = zedControlColor;

        //    //dotControl.XPointLeft.Size = new Size(zedCt1.Width / 6, zedCt1.Height / 24);
        //    //dotControl.XPointRange.Size = new Size(zedCt1.Width / 4, zedCt1.Height / 24);
        //    //dotControl.XPointRight.Size = new Size(zedCt1.Width / 4, zedCt1.Height / 24);

        //    ////dotControl.XPointLeft.Location = new Point(5,5);
        //    //dotControl.XPointRange.Location = new Point(dotControl.XPointLeft.Right);
        //    //dotControl.XPointRight.Location = new Point(zedCt1.Width - zedCt1.Width / 6);

        //    //dotControl.XPointLeft.ForeColor = graphFontColor;
        //    //dotControl.XPointRange.ForeColor = graphFontColor;
        //    //dotControl.XPointRight.ForeColor = graphFontColor;

        //    //dotControl.XPointLeft.Font = new Font("宋体", 7f);
        //    //dotControl.XPointRange.Font = new Font("宋体", 7f);
        //    //dotControl.XPointRight.Font = new Font("宋体", 7f);

        //    //dotControl.XPointLeft.Text = "12345678";
        //    //dotControl.XPointRange.Text = "123456";
        //    //dotControl.XPointRight.Text = "123456";
        //    #endregion
        //}

        //private void AddZedControlTwoGraph(PanelEx panelEx1, DotControls dotControl,ZedGraphControl graph1,ZedGraphControl graph2)
        //{
        //    if (!panelEx1.Contains(graph1))
        //        panelEx1.Controls.Add(graph1);
        //    if (!panelEx1.Contains(graph2))
        //        panelEx1.Controls.Add(graph2);

        //    graph1.Dock = DockStyle.None;
        //    graph2.Dock = DockStyle.None;

        //    #region zedControl add 
        //    graph1.Size = new Size(graphWidth,graphHeight);
        //    graph2.Size = new Size(graphWidth, graphHeight);

        //    if (IsGraphDivOrUnion)
        //    {
        //        ///分开
        //        ///
        //        if (IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //        {
        //            ///上下
        //            graph2.Location = new Point(graph1.Left, graph1.Bottom);
        //        }
        //        else
        //        {
        //            if (IsGraphLevelOrVertical && !ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                //左右
        //                graph2.Location = new Point(graph1.Right);
        //            }
        //            else if (!IsGraphLevelOrVertical && !ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                //上下
        //                graph2.Location = new Point(graph1.Left, graph1.Bottom);
        //            }
        //            else if (!IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                ///上下
        //                graph2.Location = new Point(graph1.Left, graph1.Bottom);
        //            }
        //        }
        //    }
        //    #endregion

        //    #region add annotion control
        //    //#region add control
        //    //if (!graph1.Contains(dotControl.XPointLeft))
        //    //    graph1.Controls.Add(dotControl.XPointLeft);
        //    //if (!graph1.Contains(dotControl.XPointRange))
        //    //    graph1.Controls.Add(dotControl.XPointRange);
        //    //if (!graph1.Contains(dotControl.XPointRight))
        //    //    graph1.Controls.Add(dotControl.XPointRight);

        //    //if (!graph2.Contains(dotControl.XtPointLeft))
        //    //    graph2.Controls.Add(dotControl.XtPointLeft);
        //    //if (!graph2.Contains(dotControl.XtPointRange))
        //    //    graph2.Controls.Add(dotControl.XtPointRange);
        //    //if (!graph2.Contains(dotControl.XtPointRight))
        //    //    graph2.Controls.Add(dotControl.XtPointRight);
        //    //#endregion

        //    //#region backcolor
        //    //dotControl.XPointLeft.BackColor = zedControlColor;
        //    //dotControl.XPointRight.BackColor = zedControlColor;

        //    //dotControl.YPointLeft.BackColor = zedControlColor;
        //    //dotControl.YPointRight.BackColor = zedControlColor;
        //    //#endregion

        //    //#region size
        //    //dotControl.XPointLeft.Size = new Size(zedCt1.Width / 6, zedCt1.Height / 17);
        //    //dotControl.XPointRange.Size = new Size(zedCt1.Width / 4, zedCt1.Height / 17);
        //    //dotControl.XPointRight.Size = new Size(zedCt1.Width / 4, zedCt1.Height / 17);

        //    //dotControl.YPointLeft.Size = new Size(zedCt2.Width / 6, zedCt1.Height / 17);
        //    //dotControl.YPointRange.Size = new Size(zedCt2.Width / 4, zedCt1.Height / 17);
        //    //dotControl.YPointRight.Size = new Size(zedCt2.Width / 5, zedCt1.Height / 17);
        //    //#endregion

        //    //#region location
        //    //dotControl.XPointRange.Location = new Point(dotControl.XPointLeft.Right);
        //    //dotControl.XPointRight.Location = new Point(zedCt1.Width - zedCt1.Width / 5);

        //    //dotControl.YPointRange.Location = new Point(dotControl.YPointLeft.Right);
        //    //dotControl.YPointRight.Location = new Point(zedCt2.Width - zedCt2.Width / 5);

        //    //#endregion

        //    //#region foreColor
        //    //dotControl.XPointLeft.ForeColor = graphFontColor;
        //    //dotControl.XPointRange.ForeColor = graphFontColor;
        //    //dotControl.XPointRight.ForeColor = graphFontColor;

        //    //dotControl.YPointLeft.ForeColor = graphFontColor;
        //    //dotControl.YPointRange.ForeColor = graphFontColor;
        //    //dotControl.YPointRight.ForeColor = graphFontColor;
        //    //#endregion

        //    //#region font
        //    //dotControl.XPointLeft.Font = new Font("宋体", 7f);
        //    //dotControl.XPointRange.Font = new Font("宋体", 7f);
        //    //dotControl.XPointRight.Font = new Font("宋体", 7f);

        //    //dotControl.YPointLeft.Font = new Font("宋体", 7f);
        //    //dotControl.YPointRange.Font = new Font("宋体", 7f);
        //    //dotControl.YPointRight.Font = new Font("宋体", 7f);
        //    //#endregion

        //    //#region sample
        //    //dotControl.XPointLeft.Text = "12345678";
        //    //dotControl.XPointRange.Text = "123456";
        //    //dotControl.XPointRight.Text = "123456";
        //    //#endregion
        //    #endregion
        //}

        //private void AddZedControlThreeGraph(PanelEx panelEx1, DotControls dotControl, ZedGraphControl graph1, ZedGraphControl graph2,ZedGraphControl graph3)
        //{
        //    if (!panelEx1.Contains(graph1))
        //        panelEx1.Controls.Add(graph1);
        //    if (!panelEx1.Contains(graph2))
        //        panelEx1.Controls.Add(graph2);
        //    if (!panelEx1.Contains(graph3))
        //        panelEx1.Controls.Add(graph3);

        //    graph1.Dock = DockStyle.None;
        //    graph2.Dock = DockStyle.None;
        //    graph3.Dock = DockStyle.None;

        //    #region zedControl add 
        //    graph1.Size = new Size(graphWidth, graphHeight);
        //    graph2.Size = new Size(graphWidth, graphHeight);
        //    graph3.Size = new Size(graphWidth, graphHeight);

        //    if (IsGraphDivOrUnion)
        //    {
        //        ///分开
        //        ///
        //        if (IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //        {
        //            ///上下
        //            graph2.Location = new Point(graph1.Left, graph1.Bottom);
        //            graph3.Location = new Point(graph2.Left, graph2.Bottom);
        //        }
        //        else
        //        {
        //            if (IsGraphLevelOrVertical && !ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                //左右
        //                graph2.Location = new Point(graph1.Right);
        //                graph3.Location = new Point(graph2.Right);
        //            }
        //            else if (!IsGraphLevelOrVertical && !ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                //上下
        //                graph2.Location = new Point(graph1.Left, graph1.Bottom);
        //                graph3.Location = new Point(graph2.Left, graph2.Bottom);
        //            }
        //            else if (!IsGraphLevelOrVertical && ComGraph.GraphTvConfig.IsVisable)
        //            {
        //                ///上下
        //                graph2.Location = new Point(graph1.Left, graph1.Bottom);
        //                graph3.Location = new Point(graph2.Left, graph2.Bottom);
        //            }
        //        }
        //    }
        //    #endregion

        //    #region add annotion control
        //    //#region add control
        //    //if (!zedCt1.Contains(dotControl.XPointLeft))
        //    //    zedCt1.Controls.Add(dotControl.XPointLeft);
        //    //if (!zedCt1.Contains(dotControl.XPointRange))
        //    //    zedCt1.Controls.Add(dotControl.XPointRange);
        //    //if (!zedCt1.Contains(dotControl.XPointRight))
        //    //    zedCt1.Controls.Add(dotControl.XPointRight);

        //    //if (!zedCt2.Contains(dotControl.YPointLeft))
        //    //    zedCt2.Controls.Add(dotControl.YPointLeft);
        //    //if (!zedCt2.Contains(dotControl.YPointRange))
        //    //    zedCt2.Controls.Add(dotControl.YPointRange);
        //    //if (!zedCt2.Contains(dotControl.YPointRight))
        //    //    zedCt2.Controls.Add(dotControl.YPointRight);

        //    //if (!zedCt3.Contains(dotControl.ZPointLeft))
        //    //    zedCt3.Controls.Add(dotControl.ZPointLeft);
        //    //if (!zedCt3.Contains(dotControl.ZPointRange))
        //    //    zedCt3.Controls.Add(dotControl.ZPointRange);
        //    //if (!zedCt3.Contains(dotControl.ZPointRight))
        //    //    zedCt3.Controls.Add(dotControl.ZPointRight);
        //    //#endregion

        //    //#region backcolor
        //    //dotControl.XPointLeft.BackColor = zedControlColor;
        //    //dotControl.XPointRight.BackColor = zedControlColor;

        //    //dotControl.YPointLeft.BackColor = zedControlColor;
        //    //dotControl.YPointRight.BackColor = zedControlColor;

        //    //dotControl.ZPointLeft.BackColor = zedControlColor;
        //    //dotControl.ZPointRight.BackColor = zedControlColor;
        //    //#endregion

        //    //#region size
        //    //dotControl.XPointLeft.Size = new Size(zedCt1.Width / 6, zedCt1.Height / 17);
        //    //dotControl.XPointRange.Size = new Size(zedCt1.Width / 4, zedCt1.Height / 17);
        //    //dotControl.XPointRight.Size = new Size(zedCt1.Width / 4, zedCt1.Height / 17);

        //    //dotControl.YPointLeft.Size = new Size(zedCt2.Width / 6, zedCt1.Height / 17);
        //    //dotControl.YPointRange.Size = new Size(zedCt2.Width / 4, zedCt1.Height / 17);
        //    //dotControl.YPointRight.Size = new Size(zedCt2.Width / 5, zedCt1.Height / 17);

        //    //dotControl.ZPointLeft.Size = new Size(zedCt3.Width / 6, zedCt1.Height / 17);
        //    //dotControl.ZPointRange.Size = new Size(zedCt3.Width / 4, zedCt1.Height / 17);
        //    //dotControl.ZPointRight.Size = new Size(zedCt3.Width / 5, zedCt1.Height / 17);
        //    //#endregion

        //    //#region location
        //    //dotControl.XPointRange.Location = new Point(dotControl.XPointLeft.Right);
        //    //dotControl.XPointRight.Location = new Point(zedCt1.Width - zedCt1.Width / 5);

        //    //dotControl.YPointRange.Location = new Point(dotControl.YPointLeft.Right);
        //    //dotControl.YPointRight.Location = new Point(zedCt2.Width - zedCt2.Width / 5);

        //    //dotControl.ZPointRange.Location = new Point(dotControl.YPointLeft.Right);
        //    //dotControl.ZPointRight.Location = new Point(zedCt3.Width - zedCt3.Width / 5);

        //    //#endregion

        //    //#region foreColor
        //    //dotControl.XPointLeft.ForeColor = graphFontColor;
        //    //dotControl.XPointRange.ForeColor = graphFontColor;
        //    //dotControl.XPointRight.ForeColor = graphFontColor;

        //    //dotControl.YPointLeft.ForeColor = graphFontColor;
        //    //dotControl.YPointRange.ForeColor = graphFontColor;
        //    //dotControl.YPointRight.ForeColor = graphFontColor;

        //    //dotControl.ZPointLeft.ForeColor = graphFontColor;
        //    //dotControl.ZPointRange.ForeColor = graphFontColor;
        //    //dotControl.ZPointRight.ForeColor = graphFontColor;
        //    //#endregion

        //    //#region font
        //    //dotControl.XPointLeft.Font = new Font("宋体", 7f);
        //    //dotControl.XPointRange.Font = new Font("宋体", 7f);
        //    //dotControl.XPointRight.Font = new Font("宋体", 7f);

        //    //dotControl.YPointLeft.Font = new Font("宋体", 7f);
        //    //dotControl.YPointRange.Font = new Font("宋体", 7f);
        //    //dotControl.YPointRight.Font = new Font("宋体", 7f);

        //    //dotControl.ZPointLeft.Font = new Font("宋体", 7f);
        //    //dotControl.ZPointRange.Font = new Font("宋体", 7f);
        //    //dotControl.ZPointRight.Font = new Font("宋体", 7f);
        //    //#endregion

        //    //#region sample
        //    //dotControl.XPointLeft.Text = "12345678";
        //    //dotControl.XPointRange.Text = "123456";
        //    //dotControl.XPointRight.Text = "123456";
        //    //#endregion
        //    #endregion
        //}

        //#region lineItem 
        //private void LineItemPropertySignalLine(string annota, PointPairList pplist,LineItem lineItem,GraphPane myPane)
        //{
        //    lineItem = myPane.AddCurve(annota, pplist, LineColor, SymbolType.None);
        //    lineItem.Line.Width = lineSize;

        //    lineItem.Line.IsAntiAlias = true;
        //    lineItem.Line.IsOptimizedDraw = true;
        //    lineItem.Line.IsSmooth = true;

        //    if (IsLineFill)
        //        lineItem.Line.Fill = new Fill(Color.White, Color.White, 45F);
        //}

        //private void LineItemPropertyTwoLine(string annota1,string annota2, PointPairList pplist1,PointPairList pplist2,
        //    LineItem line1,LineItem line2,GraphPane pane1,GraphPane pane2)
        //{
        //    line1 = pane1.AddCurve(annota1, pplist1, LineColor, SymbolType.None);
        //    line2 = pane1.AddCurve(annota2, pplist2, LineColor, SymbolType.None);

        //    line1.Line.Width = lineSize;
        //    line2.Line.Width = lineSize;

        //    line1.Line.IsAntiAlias = true;
        //    line1.Line.IsOptimizedDraw = true;
        //    line1.Line.IsSmooth = true;

        //    line2.Line.IsAntiAlias = true;
        //    line2.Line.IsOptimizedDraw = true;
        //    line2.Line.IsSmooth = true;

        //    if (IsLineFill)
        //    {
        //        line1.Line.Fill = new Fill(Color.White, Color.White, 45F);
        //        line2.Line.Fill = new Fill(Color.White, Color.White, 45F);
        //    }
        //}

        //private void LineItemPropertyThreeLine(string annota1, string annota2,string annota3, PointPairList pplist1, PointPairList pplist2,
        //    PointPairList pplist3,LineItem line1, LineItem line2,LineItem line3, GraphPane pane1, GraphPane pane2,GraphPane pane3)
        //{
        //    line1 = pane1.AddCurve(annota1, pplist1, LineColor, SymbolType.None);
        //    line2 = pane1.AddCurve(annota2, pplist2, LineColor, SymbolType.None);
        //    line3 = pane1.AddCurve(annota3, pplist3, LineColor, SymbolType.None);

        //    line1.Line.Width = lineSize;
        //    line2.Line.Width = lineSize;
        //    line3.Line.Width = lineSize;

        //    line1.Line.IsAntiAlias = true;
        //    line1.Line.IsOptimizedDraw = true;
        //    line1.Line.IsSmooth = true;

        //    line2.Line.IsAntiAlias = true;
        //    line2.Line.IsOptimizedDraw = true;
        //    line2.Line.IsSmooth = true;

        //    line3.Line.IsAntiAlias = true;
        //    line3.Line.IsOptimizedDraw = true;
        //    line3.Line.IsSmooth = true;

        //    if (IsLineFill)
        //    {
        //        line1.Line.Fill = new Fill(Color.White, Color.White, 45F);
        //        line2.Line.Fill = new Fill(Color.White, Color.White, 45F);
        //        line3.Line.Fill = new Fill(Color.White, Color.White, 45F);
        //    }
        //}
        //#endregion
    }
}
