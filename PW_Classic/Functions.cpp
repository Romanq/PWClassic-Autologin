#include "Functions.h"


oGetDialog Functions::GetDialogFunc = (UIObject*(__thiscall*)(void*, char*))0x737400;

oGetWindow Functions::GetWindowFunc = (DWORD(__thiscall*)(void*, char*))0x0072FB20;

oShowWindow Functions::ShowWindowFunc = (DWORD(__thiscall*)(void*, int, int, int))0x00735C50;

void UIObject::SetColor(DWORD dwColor)
{
	(void(__thiscall*)(void*, DWORD))(this, dwColor);
}

UIObject* UIObject::SetText(char* Text)
{
	return Functions::GetDialogFunc(this, Text); //Same params etc as Getdialog(ecx, name)
}