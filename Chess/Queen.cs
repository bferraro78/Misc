using System;
using System.Collections.Generic;

namespace Chess {
	class Queen : chessPiece {
		
		protected static string type = "Q";

		public Queen(bool isBlack, int locX, int locY) : base(isBlack, locX, locY) {
		}		

		public override string getType() {
			return type;
		}

		public override bool validateMove(Coord oldLoc, int newX, int newY, Board board, bool check) {
			List<Coord> validMove = allvalidMoves(board, oldLoc.getX(), oldLoc.getY(), newX, newY, true, check);

			// Was the newX/newY avaible on any of the Piece's valid paths
			if (validMove.Count != 0) {
				return true;
			}
				return false;

		}

		

		/* FOR CHECKMATE PURPOSES */
		public override List<Coord> allvalidMoves(Board board, int startX, int startY, int newX, int newY, bool isBlack, bool check) {
			List<Coord> ret = new List<Coord>();

			/* Gather Directional Movements */
			/* Left */
			for (int i = startY-1; i >= 0; i--) {
				if (board.getPiece(startX, i) == null) {
					if (startX == newX && i == newY && !check) {
						ret.Add(new Coord(startX, i));
						break;
					}
					if (check) {
						// add space, don't break
						ret.Add(new Coord(startX, i));
					}
				} else { // piece there
					if (startX == newX && i == newY) {
						ret.Add(new Coord(startX, i));
						break;
					} else {
						break;
					}
				}
			}

			/* Right */
			for (int i = startY+1; i < 8; i++) {
				if (board.getPiece(startX, i) == null) {
					if (startX == newX && i == newY && !check) {
						ret.Add(new Coord(startX, i));
						break;
					}
					if (check) {
						// add space don't break
						ret.Add(new Coord(startX, i));
					}
				} else { // piece there
					if (startX == newX && i == newY) {
						ret.Add(new Coord(startX, i));
						break;
					} else {
						break;
					}
				}
			}

			/* Down */
			for (int i = startX+1; i < 8; i++) {
				if (board.getPiece(i, startY) == null) {
					if (i == newX && startY == newY && !check) {
						ret.Add(new Coord(i, startY));
						break;
					}

					if (check) {
						// add space don't break
						ret.Add(new Coord(i, startY));
					}
				} else { // piece there
					if (i == newX && startY == newY) {
						ret.Add(new Coord(i, startY));
						break;
					} else {
						break;
					}
				}
			}

			/* Up */
			for (int i = startX-1; i >= 0; i--) {
				if (board.getPiece(i, startY) == null) {
					if (i == newX && startY == newY && !check) {
						ret.Add(new Coord(i, startY));
						break;
					}
					if (check) {
						// add space don't break
						ret.Add(new Coord(i, startY));
					}
				} else { // piece there
					if (i == newX && startY == newY) {
						ret.Add(new Coord(i, startY));
						break;
					} else {
						break;
					}
				}
			}


			/* Check Diagonal Directions */
			/* Down-Right */
			int tmpX = startX+1;
			int tmpY = startY+1;
			while (tmpX < 8 && tmpY < 8) {
				if (board.getPiece(tmpX, tmpY) == null) { 
					if (tmpX == newX && tmpY == newY && !check) {
						ret.Add(new Coord(tmpX, tmpY));
						break;
					}
					if (check) {
						// add space don't break
						ret.Add(new Coord(tmpX, tmpY));
					}
						tmpX++;
						tmpY++;
				} else { // piece there
					if (tmpX == newX && tmpY == newY) {
						ret.Add(new Coord(tmpX, tmpY));
						break;
					} else { 
						break;
					}
				}
			}

			/* Down-Left */
			tmpX = startX+1;
			tmpY = startY-1;
			while (tmpX < 8 && tmpY >= 0) {
				if (board.getPiece(tmpX, tmpY) == null) { 
					if (tmpX == newX && tmpY == newY && !check) {
						ret.Add(new Coord(tmpX, tmpY));
						break;
					}
					if (check) {
						// add space don't break
						ret.Add(new Coord(tmpX, tmpY));
					}
						tmpX++;
						tmpY--;
				} else { // piece there
					if (tmpX == newX && tmpY == newY) {
						ret.Add(new Coord(tmpX, tmpY));
						break;
					} else { // not king, don't add
						break;
					}
				}	
			}
		
			/* Up-Right */
			tmpX = startX-1;
			tmpY = startY+1;
			while (tmpX >= 0 && tmpY < 8) {
				if (board.getPiece(tmpX, tmpY) == null) {
					if (tmpX == newX && tmpY == newY && !check) {
						ret.Add(new Coord(tmpX, tmpY));
						break;
					}
					if (check) {
						// add space don't break
						ret.Add(new Coord(tmpX, tmpY));
					}
						tmpX--;
						tmpY++;
				} else { // piece there
					if (tmpX == newX && tmpY == newY) {
						ret.Add(new Coord(tmpX, tmpY));
						break;
					} else { // not king, don't add
						break;
					}
				}
			}
			/* Up-Left */
			tmpX = startX-1;
			tmpY = startY-1;
			while (tmpX >= 0 && tmpY >= 0) {
				if (board.getPiece(tmpX, tmpY) == null) { 
					if (tmpX == newX && tmpY == newY && !check) {
						ret.Add(new Coord(tmpX, tmpY));
						break;
					}
					if (check) {
						// add space don't break
						ret.Add(new Coord(tmpX, tmpY));
					}
						tmpX--;
						tmpY--;
				} else { // piece there
					if (tmpX == newX && tmpY == newY) {
						ret.Add(new Coord(tmpX, tmpY));
						break;
					} else { // not king, don't add
						break;
					}
				}
			}
			return ret;
		}

	}
}