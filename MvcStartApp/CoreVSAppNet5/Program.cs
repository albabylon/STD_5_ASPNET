using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace CoreVSAppNet5
{
    public class Program
    {
        /// <summary>
        ///  ����� ����� - ����� Main
        /// </summary>
        public static void Main(string[] args)
        {
            //��� ������� Core-���������� ��� ��������� ����, � ���� �������� ��������� ������ IHost. ��� �� ����� ��������, ������ � ������.
            //����� � ����� IHost ���������� ������ Build() � Run() ��� ��� ������� ����������
            CreateHostBuilder(args).Build().Run();
            //������ ���� ���������� ��������, � ������ ������� HTTP-�������.
            //����� ������� ��� ����������, ����� ���������� � ������ Startup.cs.
            //��� ����������� �������� ������ ����������� ���������� ��������, � ����� ������������ HTTP-�������� �� ����������� ���������. 
        }

        /// <summary>
        /// ����������� �����, ��������� � ������������� IHostBuilder -
        /// ������, ������� � ���� ������� ������� ���� ��� ������������� ������ ����������
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //�������� ��������, ������������ Host.CreateDefaultBuilder(args):
        //- ��������������� �������� ����� - ������� ����������, ��� ��� ������ ������� ����� �������������� ����� ������ �������(��������, ��� - ������� ��� �����������).
        //- ������������ ���������� ����� � ��������� ��������� ������.
        //- ����������� ������������ ���������� �� ������ appsettings.json.
        //- ������������ �������� �����������.

        //ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        //- � ������������ ����������� ���������� �����. ������ �� ������ appsettings.json: "ASPNETCORE_ENVIRONMENT": "Development"
        //- ����������� ��� ����� ������������������ ������ Kestrel, �� ������� ����� ��������� ����������.
        //- ����������� ��������� Host Filtering, ����������� ����������� ��� Kestrel ���-������.
        //- ���� ���������� ����� ���������� � Windows-��������� �� IIS, �� ����� ����� ����������� ���������� � IIS.

        //������ ������� ������ webBuilder.UseStartup<Startup>() ����� �������� � ��������� ����� Startup,
        //� ������� ��������������� ����������� ������������� �������.
    }
}
