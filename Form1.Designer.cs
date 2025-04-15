namespace OldPhoneApp
{
    partial class Form1
    {
  
        private System.ComponentModel.IContainer components = null;

        // Disposes the components when the form is closed
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout(); // Suspend layout changes until all components are initialized
            AutoScaleDimensions = new SizeF(12F, 32F); // Set default scaling dimensions
            AutoScaleMode = AutoScaleMode.Font; // Use font scaling
            ClientSize = new Size(594, 768); // Set the window size
            Margin = new Padding(6, 6, 6, 6); // Set margin for layout
            Name = "Form1"; // Set the name of the form
            Text = "Old Phone App"; // Set the title of the form

            Load += Form1_Load; // Handle the form load event
            ResumeLayout(false); // Resume layout changes
        }
    }
}

