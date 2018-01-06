using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPossessable {

	void Possess();
	void Glow(Color color);
	void UnGlow();
}
