using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjClipper.Compiler;
using AjClipper.Commands;
using System.IO;

namespace AjClipper.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        public String Result { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Machine machine = new Machine();
                Parser parser;

                parser = new Parser(TextBox1.Text);

                ICommand command = parser.ParseCommandList();

                StringWriter writer = new StringWriter();

                lock (System.Console.Out)
                {
                    TextWriter originalWriter = System.Console.Out;

                    System.Console.SetOut(writer);

                    command.Execute(machine, machine.Environment);

                    System.Console.SetOut(originalWriter);

                    this.Result = writer.ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
