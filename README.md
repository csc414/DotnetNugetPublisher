# DotnetNugetPublisher
# 一键打包&发布Nuget包
## 配置

1. 下载并解压Release.zip
2. 编辑 DotnetNugetPublisher.exe.config 配置好Nuget服务器的Source和ApiKey
3. 打开Visual Studio -> 菜单 -> 工具 -> 外部工具 -> 添加
4. 如图配置好相关路径和参数即可
![Screenshot](https://github.com/csc414/DotnetNugetPublisher/blob/master/config.png)
5. 在解决方案资源管理器选中要打包&发布的项目 -> 菜单 -> 工具 -> 发布到Nuget服务器 (这个过程尽量不要中断,比如选中项目后焦点在编辑器中停留然后再点菜单发布,这样会以编辑器中的文件所在的项目进行打包&发布)
