#include <Windows.h>
#include <iostream>
#include <string>
#include <iterator>
#include <sstream>
#include "Hooks.h"
#include <dinput.h>
#include <tchar.h>
#include <stdlib.h>
#include "detours.h"
#pragma comment(lib, "../PW_Classic/detours.lib")


HMODULE DLL_Module = 0;



DWORD _stdcall MainThread(LPVOID param)
{
	printf("[MainThread] Application startuped\n");
	
	DetourTransactionBegin();
	DetourUpdateThread(GetCurrentThread());

	//0x737400 GetDialog
	DetourAttach(&(PVOID&)Functions::GetWindowFunc, &hGetWindowFunc);

	DetourTransactionCommit();


	return false;
}

BOOL APIENTRY DllMain(HMODULE module, DWORD reason, LPVOID lpReserved)
{
	switch (reason)
	{
		case DLL_PROCESS_ATTACH:
			/*AllocConsole();
			freopen("CONOUT$", "w", stdout);
			freopen("CONIN$", "r", stdin);*/
			printf("[DLL] DLL_PROCESS_ATTACHED\n");
			DisableThreadLibraryCalls(module);
			DLL_Module = module;
			CreateThread(0, 0, MainThread, module, 0, 0); // DLL create hthread
			break;

		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
			break;
	}
	return TRUE;
}