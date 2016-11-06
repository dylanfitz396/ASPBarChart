using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;

namespace ASPColumnChart
{
    public partial class WebForm1 : Page
    {
        StringBuilder str = new StringBuilder();
        //Get connection string from web.config
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[
            "Database1ConnectionString1"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                BindChart();
            }
        }

        private DataTable GetData()
        {
            var dt = new DataTable();
            var cmd = "select Skill,ExpertiseLevel from Skills where EmpName = 'Chris'";
            var adp = new SqlDataAdapter(cmd, conn);
            adp.Fill(dt);
            return dt;
        }

        private void BindChart()
        {
            var dt = new DataTable();
            try
            {
                dt = GetData();

                str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChart);
                       function drawChart() {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Skill');
        data.addColumn('number', 'ExpertiseLevel');     
 
        data.addRows(" + dt.Rows.Count + ");");

                for (var i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["Skill"] + "');");
                    str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["ExpertiseLevel"] + ") ;");
                }

                //str.Append("<canvas id="+"bar"+" width="+"800"+" height="+"400"+"></canvas>");
                str.Append(" var chart = new google.visualization.BarChart(document.getElementById('chart_div'));");
                //str.Append(" var gradient = ctx.createLinearGradient(0, 0, 0, 400); gradient.addColorStop(0, 'rgba(151,187,205,0.7)'); gradient.addColorStop(1, 'rgba(151,187,205,0)'); ");
                str.Append(" chart.draw(data, {width: 600, height: 500, title: 'Your Skill Chart',");
                str.Append("hAxis: {title: 'Level of Expertise', titleTextStyle: {color: 'green'}},");
                str.Append("colors: ['#006C01'],");
                str.Append("animation: {duration: 1500, startup: true},");
                str.Append("}); }");
                str.Append("</script>");
                lt.Text = str.ToString().Replace('*', '"');
            }
            catch
            {
            }
        }
    }
}