# 一键打包&发布
## 配置

1. 下载并解压Release.zip
2. 编辑 DotnetNugetPublisher.exe.config 配置好Nuget服务器的Source和ApiKey
3. 打开Visual Studio -> 菜单 -> 工具 -> 外部工具 -> 添加
4. 如图配置好相关路径和参数即可
![Screenshot](https://github.com/csc414/DotnetNugetPublisher/blob/master/config.png)
5. 在解决方案资源管理器选中要打包&发布的项目 -> 菜单 -> 工具 -> 发布到Nuget服务器
