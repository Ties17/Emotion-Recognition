namespace Robotics
{
    partial class CameraCapture
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LiveImage = new Emgu.CV.UI.ImageBox();
            this.CapButton = new System.Windows.Forms.Button();
            this.CapImage = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.LiveImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CapImage)).BeginInit();
            this.SuspendLayout();
            // 
            // LiveImage
            // 
            this.LiveImage.Location = new System.Drawing.Point(88, 74);
            this.LiveImage.Name = "LiveImage";
            this.LiveImage.Size = new System.Drawing.Size(480, 360);
            this.LiveImage.TabIndex = 2;
            this.LiveImage.TabStop = false;
            this.LiveImage.Click += new System.EventHandler(this.ImageBox_Click);
            // 
            // CapButton
            // 
            this.CapButton.Location = new System.Drawing.Point(88, 462);
            this.CapButton.Name = "CapButton";
            this.CapButton.Size = new System.Drawing.Size(966, 180);
            this.CapButton.TabIndex = 3;
            this.CapButton.Text = "Capture picture";
            this.CapButton.UseVisualStyleBackColor = true;
            this.CapButton.Click += new System.EventHandler(this.CapButton_Click);
            // 
            // CapImage
            // 
            this.CapImage.Location = new System.Drawing.Point(574, 74);
            this.CapImage.Name = "CapImage";
            this.CapImage.Size = new System.Drawing.Size(480, 360);
            this.CapImage.TabIndex = 2;
            this.CapImage.TabStop = false;
            // 
            // CameraCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 711);
            this.Controls.Add(this.CapImage);
            this.Controls.Add(this.CapButton);
            this.Controls.Add(this.LiveImage);
            this.Name = "CameraCapture";
            this.Text = "CameraCapture";
            ((System.ComponentModel.ISupportInitialize)(this.LiveImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CapImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Emgu.CV.UI.ImageBox LiveImage;
        private System.Windows.Forms.Button CapButton;
        private Emgu.CV.UI.ImageBox CapImage;
    }
}