using System.Collections;

namespace AOC25.Day9;

public class Part2() : BasePart(9,2)
{
    public override string Run()
    {
        var input = Input().Select(x => x.Split(',').Select(int.Parse).ToArray()).Select(x => new Point(x[0],x[1])).ToList();

        var boundaries = new List<(Point A, Point B)>();
        for (int x = 0; x < input.Count-1; x++)
        {
            boundaries.Add((input[x], input[x+1]));
        }
        boundaries.Add((input[^1], input[0]));


        var maxArea = 0L;
        for(int i = 0; i < input.Count-1; i++)
        for (int j = i + 1; j < input.Count; j++)
        {
            var a = input[i];
            var b = input[j];
            var area = (long)(Math.Abs(a.X - b.X) + 1) * (Math.Abs(a.Y - b.Y) + 1);
            if (area <= maxArea) continue;
            if (IsBoundaryIntersecting(a.X, b.X, a.Y, b.Y, boundaries)) continue;
            if (!IsWithinShape(a.X, b.X, a.Y, b.Y, boundaries)) continue;

            maxArea = area;
        }

        return maxArea.ToString();
    }

    private bool IsWithinShape(int x1, int x2, int y1, int y2, List<(Point A, Point B)> boundaries)
    {
        var crossings = 0;
        var middlePoint = new Point((x1 + x2) / 2, (y1 + y2) / 2);

        var boundariesToCheck = boundaries.Where(b =>
            {
                if(b.A.Y == b.B.Y)
                {
                    // horizontal line
                    return false;
                }
                var b1 = new Point(b.A.X, Math.Min(b.A.Y, b.B.Y));
                var b2 = new Point(b.A.X, Math.Max(b.A.Y, b.B.Y));
                return b1.X < middlePoint.X &&
                       b1.Y <= middlePoint.Y && b2.Y > middlePoint.Y;
            }).ToList();
        crossings = boundariesToCheck.Count;

        return crossings % 2 == 1;
    }

    private bool IsBoundaryIntersecting(int x1, int x2,int y1, int y2, List<(Point A, Point B)> boundaries)
    {
        var a = new Point(Math.Min(x1, x2), Math.Min(y1, y2));
        var b = new Point(Math.Max(x1, x2), Math.Min(y1, y2));
        var c = new Point(Math.Max(x1, x2), Math.Max(y1, y2));
        var d = new Point(Math.Min(x1, x2), Math.Max(y1, y2));

        foreach (var boundary in boundaries)
        {
            if(boundary.A.X == boundary.B.X)
            {
                // vertical line
                var b1 = new Point(boundary.A.X, Math.Min(boundary.A.Y, boundary.B.Y));
                var b2 = new Point(boundary.A.X, Math.Max(boundary.A.Y, boundary.B.Y));
                if (a.X < b1.X && b.X > b1.X)
                {
                    if(a.Y > b1.Y && a.Y < b2.Y && d.Y < b2.Y) return true; // between a-b
                    if(d.Y > b1.Y && d.Y < b2.Y && a.Y < b1.Y) return true; // between d-c
                    if(a.Y < b1.Y && d.Y > b2.Y) return true; // internal completely inside
                    if(a.Y == b1.Y && d.Y > b2.Y) return true; // internal touching top
                    if(d.Y == b2.Y && a.Y < b1.Y) return true; // internal touching bottom
                    if(a.Y == b1.Y && d.Y == b2.Y) return true; // internal touching top and bottom
                    if(a.Y > b1.Y && d.Y < b2.Y) return true; // cut thru
                    if(a.Y == b1.Y && d.Y < b2.Y) return true; // cut thru touching top
                    if(d.Y == b2.Y && a.Y > b1.Y) return true; // cut thru touching bottom
                }
                // between a-b
                /*
                 *      |
                 * A####|####B
                 * #    |    #
                 * #         #
                 * D#########C
                 *
                 * a.X < line
                 * b.X > line
                 * a.Y > lineTop.Y
                 * a.Y < lineBottom.Y
                 * d.Y > lineBottom.Y
                 */
                // between d-c
                /*
                 *
                 * A#########B
                 * #         #
                 * #    |    #
                 * D####|####C
                 *      |
                 *
                 * d.X < line
                 * c.X > line
                 * a.Y < lineTop.Y
                 * d.Y < lineBottom.Y
                 * d.Y > lineTop.Y
                 */
                // internal completely inside
                /*
                 *
                 * A#########B
                 * #         #
                 * #    |    #
                 * #         #
                 * D#########C
                 *
                 * a.X > line
                 * b.X < line
                 * d.Y > lineBottom.Y
                 * a.Y < lineTop.Y
                 */
                // internal touching top
                /*
                 *
                 * A####|####B
                 * #    |    #
                 * #    |    #
                 * #         #
                 * D#########C
                 *
                 * a.X > line
                 * b.X < line
                 * a.Y == lineTop.Y
                 * d.Y > lineBottom.Y
                 */
                // internal touching bottom
                /*
                 *
                 * A#########B
                 * #         #
                 * #    |    #
                 * #    |    #
                 * D####|####C
                 *
                 * d.X > line
                 * c.X < line
                 * a.Y < lineTop.Y
                 * d.Y == lineBottom.Y
                 */
                // internal touching top and bottom
                /*
                 *
                 * A####|####B
                 * #    |    #
                 * #    |    #
                 * #    |    #
                 * D####|####C
                 *
                 * a.X > line
                 * b.X < line
                 * a.Y == lineTop.Y
                 * d.Y == lineBottom.Y
                 */
                // cut thru
                /*
                 *      |
                 * A####|####B
                 * #    |    #
                 * #    |    #
                 * #    |    #
                 * D####|####C
                 *      |
                 *
                 * a.X < line
                 * b.X > line
                 * a.Y > lineTop.Y
                 * d.Y < lineBottom.Y
                 */
                // cut thru touching top
                /*
                 *
                 * A####|####B
                 * #    |    #
                 * #    |    #
                 * #    |    #
                 * D####|####C
                 *      |
                 *
                 * a.X < line
                 * b.X > line
                 * a.Y == lineTop.Y
                 * d.Y < lineBottom.Y
                 */
                // cut thru touching bottom
                /*
                 *      |
                 * A####|####B
                 * #    |    #
                 * #    |    #
                 * #    |    #
                 * D####|####C
                 *
                 * a.X < line
                 * b.X > line
                 * a.Y > lineTop.Y
                 * d.Y == lineBottom.Y
                 */
            }
            else
            {
                // horizontal line
                var b1 = new Point(Math.Min(boundary.A.X, boundary.B.X), boundary.A.Y);
                var b2 = new Point(Math.Max(boundary.A.X, boundary.B.X), boundary.A.Y);

                if(a.Y < b1.Y && d.Y > b1.Y)
                {
                    if(a.X > b1.X && a.X < b2.X && b.X > b2.X) return true; // between a-b
                    if(b.X > b1.X && b.X < b2.X && a.X < b1.X) return true; // between b-c
                    if(a.X < b1.X && b.X > b2.X) return true; // internal completely inside
                    if(a.X == b1.X && b.X > b2.X) return true; // internal touching left edge
                    if(b.X == b2.X && a.X < b1.X) return true; // internal touching right edge
                    if(a.X == b1.X && b.X == b2.X) return true; // internal touching left and right edge
                    if(a.X > b1.X && b.X < b2.X) return true; // cut thru
                    if(a.X == b1.X && b.X < b2.X) return true; // cut thru touching left edge
                    if(b.X == b2.X && a.X > b1.X) return true; // cut thru touching right edge
                }
                // between b-c
                /*
                 *
                 * A#########B
                 * #         #
                 * #      ------
                 * #         #
                 * D#########C
                 *
                 */
                // between a-d
                /*
                 *
                 *  A#########B
                 *  #         #
                 *-----       #
                 *  #         #
                 *  D#########C
                 *
                 */
                // internal completely inside
                /*
                 *
                 * A#########B
                 * #         #
                 * #  -----  #
                 * #         #
                 * D#########C
                 *
                 */
                // internal touching left edge
                /*
                 *
                 * A#########B
                 * #         #
                 * -----     #
                 * #         #
                 * D#########C
                 *
                 */
                // internal touching right edge
                /*
                 *
                 * A#########B
                 * #         #
                 * #     -----
                 * #         #
                 * D#########C
                 *
                 */
                // internal touching left and right edge
                /*
                 *
                 * A#########B
                 * #         #
                 * -----------
                 * #         #
                 * D#########C
                 *
                 */
                // cut thru
                /*
                 *
                 *   A#########B
                 *   #         #
                 * ---------------
                 *   #         #
                 *   D#########C
                 *
                 */
                // cut thru touching left edge
                /*
                 *
                 *   A#########B
                 *   #         #
                 *   -------------
                 *   #         #
                 *   D#########C
                 *
                 */
                // cut thru touching right edge
                /*
                 *
                 *   A#########B
                 *   #         #
                 * -------------
                 *   #         #
                 *   D#########C
                 *
                 */
            }
        }
        return false;
    }
}
