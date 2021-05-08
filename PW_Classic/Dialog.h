#pragma once
#include <Windows.h>
#include "Functions.h"

class UIObject
{
public:
	char pad_0004[188]; //0x0004

	virtual void Function0();
	virtual void Function1();
	virtual void Function2();
	virtual void Function3();
	virtual void Function4();
	virtual void Function5();
	virtual void Function6();
	virtual void Function7();
	virtual void Function8();
	virtual void Function9();
	virtual void Function10();
	virtual void SetColor(DWORD dwColor);
	virtual void Function12();
	virtual UIObject* SetText(char* Text);
	virtual void Function14();
}; //Size: 0x00C0
