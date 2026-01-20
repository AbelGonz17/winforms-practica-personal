using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

[DefaultEvent("TextChanged")]
public class RoundedTextBox : UserControl
{
    private TextBox textBox = new TextBox();
    private Timer animationTimer = new Timer();

    private int borderRadius = 15;
    private int borderSize = 2;
    private int animatedBorderSize = 2;

    private Color borderColor = Color.Gray;
    private Color borderFocusColor = Color.MediumSlateBlue;
    private Color currentBorderColor;

    private string placeholderText = "Placeholder...";
    private Color placeholderColor = Color.DarkGray;
    private bool isPlaceholder = true;
    private bool isPassword = false;

    public RoundedTextBox()
    {
        this.Size = new Size(200, 35);
        this.DoubleBuffered = true;
        this.BackColor = Color.White;

        // Configuración del TextBox interno
        textBox.BorderStyle = BorderStyle.None;
        textBox.Font = this.Font;
        textBox.BackColor = this.BackColor;
        textBox.TabStop = true;

        // Eventos
        textBox.Enter += RemovePlaceholder;
        textBox.Leave += SetPlaceholder;
        textBox.TextChanged += (s, e) => OnTextChanged(e);

        this.Controls.Add(textBox);

        currentBorderColor = borderColor;
        animatedBorderSize = borderSize;

        animationTimer.Interval = 15;
        animationTimer.Tick += AnimateBorder;

        SetPlaceholder(null, null);
        UpdateBaseTextBoxPosition(); // Ajustar posición inicial
    }

    // ================= LÓGICA DE CENTRADO =================

    private void UpdateBaseTextBoxPosition()
    {
        // Calculamos el centro vertical
        // Altura total - altura del texto / 2
        int txtTop = (this.Height - textBox.Height) / 2;

        // Ajustamos el margen horizontal según el radio para que no toque los bordes curvos
        int txtLeft = borderRadius / 2 + 5;
        int txtWidth = this.Width - (txtLeft * 2);

        textBox.Location = new Point(txtLeft, txtTop);
        textBox.Width = txtWidth;
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        UpdateBaseTextBoxPosition();
        this.Invalidate();
    }

    protected override void OnFontChanged(EventArgs e)
    {
        base.OnFontChanged(e);
        textBox.Font = this.Font;
        UpdateBaseTextBoxPosition(); // Recalcular si cambia el tamaño de letra
    }

    // ================= PROPIEDADES =================

    [Category("Rounded TextBox")]
    public HorizontalAlignment TextAlign
    {
        get => textBox.TextAlign;
        set => textBox.TextAlign = value;
    }

    [Category("Rounded TextBox")]
    public override string Text
    {
        get => isPlaceholder ? string.Empty : textBox.Text;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) SetPlaceholder(null, null);
            else
            {
                isPlaceholder = false;
                textBox.ForeColor = this.ForeColor;
                textBox.Text = value;
                textBox.UseSystemPasswordChar = isPassword;
            }
            Invalidate();
        }
    }

    // (Otras propiedades se mantienen igual)
    [Category("Rounded TextBox")] public int BorderRadius { get => borderRadius; set { borderRadius = value; UpdateBaseTextBoxPosition(); Invalidate(); } }
    [Category("Rounded TextBox")] public int BorderSize { get => borderSize; set { borderSize = value; animatedBorderSize = value; Invalidate(); } }
    [Category("Rounded TextBox")] public Color BorderColor { get => borderColor; set { borderColor = value; if (!textBox.Focused) currentBorderColor = value; Invalidate(); } }
    [Category("Rounded TextBox")] public Color BorderFocusColor { get => borderFocusColor; set => borderFocusColor = value; }
    [Category("Rounded TextBox")] public string PlaceholderText { get => placeholderText; set { placeholderText = value; if (isPlaceholder) textBox.Text = value; } }
    [Category("Rounded TextBox")] public bool PasswordChar { get => isPassword; set { isPassword = value; if (!isPlaceholder) textBox.UseSystemPasswordChar = value; } }
    public override Color BackColor { get => base.BackColor; set { base.BackColor = value; textBox.BackColor = value; } }

    // ================= MÉTODOS PRIVADOS =================

    private void RemovePlaceholder(object sender, EventArgs e)
    {
        if (isPlaceholder)
        {
            isPlaceholder = false;
            textBox.Text = "";
            textBox.ForeColor = this.ForeColor;
            textBox.UseSystemPasswordChar = isPassword;
        }
        animationTimer.Start();
    }

    private void SetPlaceholder(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            isPlaceholder = true;
            textBox.Text = placeholderText;
            textBox.ForeColor = placeholderColor;
            textBox.UseSystemPasswordChar = false;
        }
        animationTimer.Start();
    }

    private void AnimateBorder(object sender, EventArgs e)
    {
        if (textBox.Focused)
        {
            currentBorderColor = borderFocusColor;
            if (animatedBorderSize < borderSize + 1) animatedBorderSize++;
            else animationTimer.Stop();
        }
        else
        {
            currentBorderColor = borderColor;
            if (animatedBorderSize > borderSize) animatedBorderSize--;
            else animationTimer.Stop();
        }
        Invalidate();
    }

    // ================= DIBUJO =================

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        Rectangle rectBorder = this.ClientRectangle;

        using (GraphicsPath pathBorder = GetRoundedPath(rectBorder, borderRadius))
        using (Pen penBorder = new Pen(currentBorderColor, animatedBorderSize))
        {
            penBorder.Alignment = PenAlignment.Inset;
            this.Region = new Region(pathBorder);
            e.Graphics.DrawPath(penBorder, pathBorder);
        }
    }

    private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        float size = radius * 2F;
        if (size > rect.Width) size = rect.Width;
        if (size > rect.Height) size = rect.Height;

        path.StartFigure();
        path.AddArc(rect.X, rect.Y, size, size, 180, 90);
        path.AddArc(rect.Right - size, rect.Y, size, size, 270, 90);
        path.AddArc(rect.Right - size, rect.Bottom - size, size, size, 0, 90);
        path.AddArc(rect.X, rect.Bottom - size, size, size, 90, 90);
        path.CloseFigure();
        return path;
    }
}