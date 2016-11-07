using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPColumnChart
{
    public partial class InputForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (txtEmpName.Text != "")
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[
                    "Database1ConnectionString1"].ConnectionString);
                {
                    SqlCommand xp =
                        new SqlCommand(
                            "Insert into Skills(Id, EmpName, Skill, ExpertiseLevel) Values(@Id, @EmpName, @Skill, @ExpertiseLevel)",
                            conn);
                    xp.Parameters.AddWithValue("@Id", "13");
                    xp.Parameters.AddWithValue("@EmpName", txtEmpName.Text);
                    xp.Parameters.AddWithValue("@Skill", txtSkill.Text);
                    xp.Parameters.AddWithValue("@ExpertiseLevel", txtExpertiseLevel.Text);

                    conn.Open();
                    xp.ExecuteNonQuery();
                    conn.Close();

                }
            }

            Response.Redirect("~/WebForm1.aspx");
        }
    }
}