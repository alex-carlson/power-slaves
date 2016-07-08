using UnityEngine;
using System.Collections;

public enum Direction {N, NE, E, SE, S, SW, W, NW};

public static class ExtensionMethods {

	public static Vector3 m_playerDir;
	
	public static Vector3 m_Dir(Direction playerDir){
		if (playerDir == Direction.N) {
			m_playerDir = new Vector3(0, 1, 0);
		} else if (playerDir == Direction.NE) {
			m_playerDir = new Vector3(1, 1, 0);
		} else if (playerDir == Direction.E) {
			m_playerDir = new Vector3(1, 0, 0);
		} else if (playerDir == Direction.SE) {
			m_playerDir = new Vector3(1, -1, 0);
		} else if (playerDir == Direction.S) {
			m_playerDir = new Vector3(0, -1, 0);
		} else if (playerDir == Direction.SW) {
			m_playerDir = new Vector3(-1, -1, 0);
		} else if (playerDir == Direction.W) {
			m_playerDir = new Vector3(-1, 0, 0);
		} else {
			m_playerDir = new Vector3(-1, 1, 0);
		}
		return m_playerDir;
	}
}
