using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OracleLotApp
{
    public class Fortune
    {
        public int No { get; set; }
        public string Level { get; set; }
        public string Poem { get; set; }
        public string Meaning { get; set; }
        public string Career { get; set; }
        public string Love { get; set; }
        public string Study { get; set; }
        public string Advice { get; set; }
    }

    public class MainForm : Form
    {
        private readonly List<Fortune> fortunes = new List<Fortune>();
        private readonly Random random = new Random();
        private readonly Timer animationTimer = new Timer();

        private int animationStep = 0;
        private Fortune pendingFortune;

        private Label lblTitle;
        private Label lblSubTitle;
        private Label lblNo;
        private Label lblLevel;
        private Label lblPoem;
        private Label lblMeaning;
        private Label lblCareer;
        private Label lblLove;
        private Label lblStudy;
        private Label lblAdvice;

        private Button btnDraw;
        private Button btnClear;
        private ListBox lstHistory;

        private RoundedPanel resultCard;
        private RoundedPanel historyListCard;

        public MainForm()
        {
            Text = "靈籤小築 - 無資料庫求籤系統";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(1120, 760);
            Size = new Size(1200, 820);
            BackColor = Color.FromArgb(246, 241, 232);
            AutoScaleMode = AutoScaleMode.Dpi;
            DoubleBuffered = true;
            Font = new Font("Microsoft JhengHei UI", 13F, FontStyle.Regular);

            InitData();
            InitUI();

            animationTimer.Interval = 70;
            animationTimer.Tick += AnimationTimer_Tick;
        }

        private void InitData()
        {
            fortunes.Add(new Fortune { No = 1, Level = "上上籤", Poem = "雲開月現照前程，積善之家福自臨。", Meaning = "目前運勢漸明，過去的努力會開始產生回報。", Career = "適合主動爭取機會，專案與面試都有加分。", Love = "關係可慢慢推進，重點是穩定與真誠。", Study = "讀書運佳，適合整理筆記與衝刺證照。", Advice = "把握時機，但不要躁進。" });
            fortunes.Add(new Fortune { No = 2, Level = "上籤", Poem = "春風送暖入門來，萬事從容漸次開。", Meaning = "事情會逐步變好，不必急著一次到位。", Career = "工作與專案宜穩扎穩打，先求品質再求速度。", Love = "互動自然即可，過度猜測反而扣分。", Study = "適合固定每日進度，長期累積會見效。", Advice = "穩定執行，就是你的優勢。" });
            fortunes.Add(new Fortune { No = 3, Level = "中上籤", Poem = "山路崎嶇終見道，一番磨練一番新。", Meaning = "短期有阻礙，但方向並沒有錯。", Career = "遇到卡關時要拆小步驟，不要整個放棄。", Love = "先觀察，不要急著下結論。", Study = "弱科需要補基礎，不能只靠臨時抱佛腳。", Advice = "先修正方法，再追求成果。" });
            fortunes.Add(new Fortune { No = 4, Level = "中籤", Poem = "欲速未能成好事，靜心守正待良辰。", Meaning = "現在不是硬衝的時候，適合整理與準備。", Career = "履歷、作品集、專案文件要先補齊。", Love = "不要被情緒帶著走，保持分寸。", Study = "適合複習舊題與建立錯題本。", Advice = "穩住節奏，別亂開新坑。" });
            fortunes.Add(new Fortune { No = 5, Level = "小吉", Poem = "一念清明通萬象，退一步處有天寬。", Meaning = "調整心態後，問題會比想像中好處理。", Career = "適合重新規劃定位，避免什麼都想做。", Love = "先把自己狀態顧好，互動才會自然。", Study = "建立固定時間，比追求爆發更可靠。", Advice = "少抱怨，多整理。" });
            fortunes.Add(new Fortune { No = 6, Level = "平籤", Poem = "流水不爭先後勢，守得初心自有成。", Meaning = "目前運勢平穩，重點在持續。", Career = "不要跟人亂比，照自己的路線做出成果。", Love = "關係平淡不是壞事，穩定比較重要。", Study = "每日固定輸出，成績會慢慢回來。", Advice = "把基本功做好。" });
            fortunes.Add(new Fortune { No = 7, Level = "中下籤", Poem = "霧裡看花難定意，暫停腳步問初心。", Meaning = "目前判斷容易受情緒影響，先不要做重大決定。", Career = "專案要先收斂，不要同時開太多系統。", Love = "避免衝動訊息與過度解讀。", Study = "先補最弱的章節，不要只讀會的地方。", Advice = "冷靜三天，再決定。" });
            fortunes.Add(new Fortune { No = 8, Level = "提醒籤", Poem = "心猿意馬多煩惱，定性安神福自招。", Meaning = "不是沒有機會，而是注意力太分散。", Career = "先完成一個能展示的作品，比做十個半成品有用。", Love = "別把對方反應當成全部答案。", Study = "手機與社群要控管，否則效率會掉。", Advice = "專注，才會轉運。" });
        }

        private void InitUI()
        {
            Controls.Clear();

            var main = new TableLayoutPanel();
            main.Dock = DockStyle.Fill;
            main.BackColor = Color.FromArgb(246, 241, 232);
            main.Padding = new Padding(28);
            main.ColumnCount = 2;
            main.RowCount = 3;
            main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 118F));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Controls.Add(main);

            BuildHeader(main);
            BuildActionBar(main);
            BuildTopHistoryTitle(main);
            BuildResultCard(main);
            BuildHistoryList(main);
        }

        private void BuildHeader(TableLayoutPanel main)
        {
            var header = new GradientPanel(Color.FromArgb(120, 64, 32), Color.FromArgb(202, 132, 58));
            header.Dock = DockStyle.Fill;
            header.Margin = new Padding(0, 0, 0, 18);
            header.Padding = new Padding(24, 14, 24, 12);
            main.SetColumnSpan(header, 2);
            main.Controls.Add(header, 0, 0);

            var headerLayout = new TableLayoutPanel();
            headerLayout.Dock = DockStyle.Fill;
            headerLayout.BackColor = Color.Transparent;
            headerLayout.RowCount = 2;
            headerLayout.ColumnCount = 1;
            headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 62F));
            headerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 38F));
            header.Controls.Add(headerLayout);

            lblTitle = new Label();
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.AutoSize = false;
            lblTitle.Text = "☯ 靈籤小築 ☯";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Microsoft JhengHei UI", 28F, FontStyle.Bold);
            headerLayout.Controls.Add(lblTitle, 0, 0);

            lblSubTitle = new Label();
            lblSubTitle.Dock = DockStyle.Fill;
            lblSubTitle.AutoSize = false;
            lblSubTitle.Text = "";
            lblSubTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblSubTitle.ForeColor = Color.FromArgb(255, 238, 210);
            lblSubTitle.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Regular);
            headerLayout.Controls.Add(lblSubTitle, 0, 1);

        }

        private void BuildActionBar(TableLayoutPanel main)
        {
            var actionBar = new RoundedPanel();
            actionBar.Dock = DockStyle.Fill;
            actionBar.BackColor = Color.White;
            actionBar.Margin = new Padding(0, 0, 18, 20);
            actionBar.Padding = new Padding(28, 24, 28, 24);
            main.Controls.Add(actionBar, 0, 1);

            var buttonLayout = new FlowLayoutPanel();
            buttonLayout.Dock = DockStyle.Fill;
            buttonLayout.FlowDirection = FlowDirection.LeftToRight;
            buttonLayout.WrapContents = false;
            buttonLayout.AutoScroll = false;
            buttonLayout.BackColor = Color.Transparent;
            buttonLayout.Padding = new Padding(0);
            actionBar.Controls.Add(buttonLayout);

            btnDraw = MakeButton(
                "開始求籤",
                250,
                68,
                new Padding(22, 10, 22, 10),
                Color.FromArgb(144, 85, 38),
                Color.White,
                true);
            btnDraw.Click += BtnDraw_Click;
            btnDraw.MouseEnter += delegate { btnDraw.BackColor = Color.FromArgb(176, 105, 45); };
            btnDraw.MouseLeave += delegate { btnDraw.BackColor = Color.FromArgb(144, 85, 38); };
            buttonLayout.Controls.Add(btnDraw);

            btnClear = MakeButton(
                "清除紀錄",
                190,
                68,
                new Padding(18, 10, 18, 10),
                Color.White,
                Color.FromArgb(132, 75, 34),
                false);
            btnClear.FlatAppearance.BorderColor = Color.FromArgb(194, 131, 64);
            btnClear.Click += delegate { lstHistory.Items.Clear(); };
            buttonLayout.Controls.Add(btnClear);
        }

        private void BuildTopHistoryTitle(TableLayoutPanel main)
        {
            var historyCard = new RoundedPanel();
            historyCard.Dock = DockStyle.Fill;
            historyCard.BackColor = Color.White;
            historyCard.Margin = new Padding(0, 0, 0, 20);
            historyCard.Padding = new Padding(22, 18, 22, 18);
            main.Controls.Add(historyCard, 1, 1);

            var histTitle = new Label();
            histTitle.Dock = DockStyle.Fill;
            histTitle.AutoSize = false;
            histTitle.Text = "求籤紀錄";
            histTitle.Font = new Font("Microsoft JhengHei UI", 17F, FontStyle.Bold);
            histTitle.ForeColor = Color.FromArgb(98, 54, 27);
            histTitle.TextAlign = ContentAlignment.MiddleLeft;
            historyCard.Controls.Add(histTitle);
        }

        private void BuildResultCard(TableLayoutPanel main)
        {
            resultCard = new RoundedPanel();
            resultCard.Dock = DockStyle.Fill;
            resultCard.BackColor = Color.White;
            resultCard.Margin = new Padding(0, 0, 18, 0);
            resultCard.Padding = new Padding(32, 26, 32, 26);
            main.Controls.Add(resultCard, 0, 2);

            var resultLayout = new TableLayoutPanel();
            resultLayout.Dock = DockStyle.Fill;
            resultLayout.BackColor = Color.Transparent;
            resultLayout.ColumnCount = 1;
            resultLayout.RowCount = 8;
            resultLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            resultLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            resultLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            resultLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            resultLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
            resultLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
            resultLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
            resultLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            resultCard.Controls.Add(resultLayout);

            lblNo = MakeResultLabel("尚未求籤", 26F, FontStyle.Bold, Color.FromArgb(98, 54, 27));
            lblLevel = MakeResultLabel("請按下『開始求籤』", 20F, FontStyle.Bold, Color.FromArgb(184, 91, 42));
            lblPoem = MakeResultLabel("靜心三秒，想著你目前最在意的事情。", 21F, FontStyle.Bold, Color.FromArgb(43, 43, 43));
            lblMeaning = MakeResultLabel("解籤：等待抽籤中。", 17F, FontStyle.Regular, Color.FromArgb(64, 64, 64));
            lblCareer = MakeResultLabel("事業：—", 16F, FontStyle.Regular, Color.FromArgb(64, 64, 64));
            lblLove = MakeResultLabel("感情：—", 16F, FontStyle.Regular, Color.FromArgb(64, 64, 64));
            lblStudy = MakeResultLabel("學業：—", 16F, FontStyle.Regular, Color.FromArgb(64, 64, 64));
            lblAdvice = MakeResultLabel("建議：—", 18F, FontStyle.Bold, Color.FromArgb(98, 54, 27));

            resultLayout.Controls.Add(lblNo, 0, 0);
            resultLayout.Controls.Add(lblLevel, 0, 1);
            resultLayout.Controls.Add(lblPoem, 0, 2);
            resultLayout.Controls.Add(lblMeaning, 0, 3);
            resultLayout.Controls.Add(lblCareer, 0, 4);
            resultLayout.Controls.Add(lblLove, 0, 5);
            resultLayout.Controls.Add(lblStudy, 0, 6);
            resultLayout.Controls.Add(lblAdvice, 0, 7);
        }

        private void BuildHistoryList(TableLayoutPanel main)
        {
            historyListCard = new RoundedPanel();
            historyListCard.Dock = DockStyle.Fill;
            historyListCard.BackColor = Color.White;
            historyListCard.Padding = new Padding(22, 24, 22, 22);
            main.Controls.Add(historyListCard, 1, 2);

            var layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.BackColor = Color.Transparent;
            layout.RowCount = 2;
            layout.ColumnCount = 1;
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            historyListCard.Controls.Add(layout);

            var h2 = new Label();
            h2.Dock = DockStyle.Fill;
            h2.AutoSize = false;
            h2.Text = "歷史列表";
            h2.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            h2.ForeColor = Color.FromArgb(98, 54, 27);
            h2.TextAlign = ContentAlignment.MiddleLeft;
            layout.Controls.Add(h2, 0, 0);

            lstHistory = new ListBox();
            lstHistory.Dock = DockStyle.Fill;
            lstHistory.Font = new Font("Microsoft JhengHei UI", 13F, FontStyle.Regular);
            lstHistory.ItemHeight = 32;
            lstHistory.BorderStyle = BorderStyle.None;
            lstHistory.BackColor = Color.White;
            layout.Controls.Add(lstHistory, 0, 1);
        }

        private Button MakeButton(
            string text,
            int width,
            int height,
            Padding padding,
            Color backColor,
            Color foreColor,
            bool strong)
        {
            var button = new Button();
            button.Text = text;
            button.Font = new Font("Microsoft JhengHei UI", strong ? 18F : 15F, FontStyle.Bold);
            button.Size = new Size(width, height);
            button.Padding = padding;
            button.Margin = new Padding(0, 0, 18, 0);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = strong ? 0 : 1;
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Cursor = Cursors.Hand;
            return button;
        }

        private Label MakeResultLabel(string text, float size, FontStyle style, Color color)
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                AutoSize = false,
                Text = text,
                Font = new Font("Microsoft JhengHei UI", size, style),
                ForeColor = color,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0, 2, 0, 2)
            };
        }

        private void BtnDraw_Click(object sender, EventArgs e)
        {
            pendingFortune = fortunes[random.Next(fortunes.Count)];
            animationStep = 0;
            btnDraw.Enabled = false;

            lblNo.Text = "正在擲筊求籤...";
            lblLevel.Text = "請稍候";
            lblPoem.Text = "籤筒搖動中";
            lblMeaning.Text = "解籤：正在整理籤意。";
            lblCareer.Text = "事業：—";
            lblLove.Text = "感情：—";
            lblStudy.Text = "學業：—";
            lblAdvice.Text = "建議：—";

            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            animationStep++;

            string dots = new string('・', (animationStep % 4) + 1);
            lblPoem.Text = "籤筒搖動中" + dots;

            resultCard.BackColor = (animationStep % 2 == 0)
                ? Color.FromArgb(255, 252, 246)
                : Color.White;

            if (animationStep >= 18)
            {
                animationTimer.Stop();
                ShowFortune(pendingFortune);
                btnDraw.Enabled = true;
                resultCard.BackColor = Color.White;
            }
        }

        private void ShowFortune(Fortune f)
        {
            lblNo.Text = "第 " + f.No + " 籤";
            lblLevel.Text = f.Level;
            lblPoem.Text = f.Poem;
            lblMeaning.Text = "解籤：" + f.Meaning;
            lblCareer.Text = "事業：" + f.Career;
            lblLove.Text = "感情：" + f.Love;
            lblStudy.Text = "學業：" + f.Study;
            lblAdvice.Text = "建議：" + f.Advice;

            lstHistory.Items.Insert(
                0,
                DateTime.Now.ToString("HH:mm") + "  第" + f.No + "籤｜" + f.Level
            );
        }
    }

    public class RoundedPanel : Panel
    {
        public RoundedPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Width <= 0 || Height <= 0)
            {
                return;
            }

            using (GraphicsPath path = RoundedRect(new Rectangle(0, 0, Width - 1, Height - 1), 24))
            {
                Region = new Region(path);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            }
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();

            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }
    }

    public class GradientPanel : RoundedPanel
    {
        private readonly Color left;
        private readonly Color right;

        public GradientPanel(Color leftColor, Color rightColor)
        {
            left = leftColor;
            right = rightColor;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Width <= 0 || Height <= 0)
            {
                return;
            }

            using (var brush = new LinearGradientBrush(ClientRectangle, left, right, 0f))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }
    }
}
