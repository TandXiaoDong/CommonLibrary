echo off
cls
bcdedit /set hypervisorlaunchtype off
echo ...
if %errorlevel% equ 0 (echo ���� Hyper-V ��Ҫ��������ϵͳ������Ч) else (echo ������Ҫʹ�ù���Ա�������)
pause