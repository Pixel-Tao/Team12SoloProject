using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 사용할지는 아직 미지수
/// </summary>
public class BTPathFinder
{
    public struct PQNode : IComparable<PQNode>
    {
        public int H; // Heuristic
        public Vector3Int CellPos;
        public int Depth;

        public int CompareTo(PQNode other)
        {
            if (H == other.H)
                return 0;
            return H < other.H ? 1 : -1;
        }
    }

    List<Vector3Int> _delta = new List<Vector3Int>()
    {
        new Vector3Int(0, 1, 0), // U
        new Vector3Int(1, 1, 0), // UR
        new Vector3Int(1, 0, 0), // R
        new Vector3Int(1, -1, 0), // DR
        new Vector3Int(0, -1, 0), // D
        new Vector3Int(-1, -1, 0), // LD
        new Vector3Int(-1, 0, 0), // L
        new Vector3Int(-1, 1, 0), // LU
    };

    private bool CanGo(GameObject self, Vector3Int cellPos)
    {
        // 벽이면 못 간다.
        // if (self.IsValidTile(cellPos))
        //     return false;

        return true;
    }

    public List<Vector3Int> FindPath(GameObject self, Vector3Int startCellPos, Vector3Int destCellPos, int maxDepth = 10,
        int addCellSize = 0)
    {
        // 지금까지 제일 좋은 후보 기록.
        Dictionary<Vector3Int, int> best = new Dictionary<Vector3Int, int>();
        // 경로 추적 용도.
        Dictionary<Vector3Int, Vector3Int> parent = new Dictionary<Vector3Int, Vector3Int>();

        // 현재 발견된 후보 중에서 가장 좋은 후보를 빠르게 뽑아오기 위한 도구.
        BTPriorityQueue<PQNode> pq = new BTPriorityQueue<PQNode>(); // OpenList

        Vector3Int pos = startCellPos;
        Vector3Int dest = destCellPos;

        // destCellPos에 도착 못하더라도 제일 가까운 애로.
        Vector3Int closestCellPos = startCellPos;
        int closestH = (dest - pos).sqrMagnitude;

        // 시작점 발견 (예약 진행)
        {
            int h = (dest - pos).sqrMagnitude;
            pq.Push(new PQNode() { H = h, CellPos = pos, Depth = 1 });
            parent[pos] = pos;
            best[pos] = h;
        }

        while (pq.Count > 0)
        {
            // 제일 좋은 후보를 찾는다
            PQNode node = pq.Pop();
            pos = node.CellPos;

            // 목적지 도착했으면 바로 종료.
            if (pos == dest)
                break;

            // 무한으로 깊이 들어가진 않음.
            if (node.Depth >= maxDepth)
                break;

            // 상하좌우 등 이동할 수 있는 좌표인지 확인해서 예약한다.
            foreach (Vector3Int delta in _delta)
            {
                //3*3 이상인 경우 addCellSize를 곱한다.
                Vector3Int next = pos + delta;

                // 갈 수 없는 장소면 스킵.
                if (CanGo(self, next) == false)
                    continue;

                // 예약 진행
                int h = (dest - next).sqrMagnitude;

                // 더 좋은 후보 찾았는지
                if (best.ContainsKey(next) == false)
                    best[next] = int.MaxValue;

                if (best[next] <= h)
                    continue;

                best[next] = h;

                pq.Push(new PQNode() { H = h, CellPos = next, Depth = node.Depth + 1 });
                parent[next] = pos;

                // 목적지까지는 못 가더라도, 그나마 제일 좋았던 후보 기억.
                if (closestH > h)
                {
                    closestH = h;
                    closestCellPos = next;
                }
            }
        }

        // 제일 가까운 애라도 찾음.
        if (parent.ContainsKey(dest) == false)
            return CalcCellPathFromParent(parent, closestCellPos);

        return CalcCellPathFromParent(parent, dest);
    }

    private List<Vector3Int> CalcCellPathFromParent(Dictionary<Vector3Int, Vector3Int> parent, Vector3Int dest)
    {
        List<Vector3Int> cells = new List<Vector3Int>();

        if (parent.ContainsKey(dest) == false)
            return cells;

        Vector3Int now = dest;

        while (parent[now] != now)
        {
            cells.Add(now);
            now = parent[now];
        }

        cells.Add(now);
        cells.Reverse();

        return cells;
    }
}

public class BTPriorityQueue<T> where T : IComparable<T>
{
    List<T> _heap = new List<T>();

    // O(logN)
    public void Push(T data)
    {
        // 힙의 맨 끝에 새로운 데이터를 삽입한다
        _heap.Add(data);

        int now = _heap.Count - 1;
        // 도장깨기를 시작
        while (now > 0)
        {
            // 도장깨기를 시도
            int next = (now - 1) / 2;
            if (_heap[now].CompareTo(_heap[next]) < 0)
                break; // 실패

            // 두 값을 교체한다
            (_heap[now], _heap[next]) = (_heap[next], _heap[now]);

            // 검사 위치를 이동한다
            now = next;
        }
    }

    // O(logN)
    public T Pop()
    {
        // 반환할 데이터를 따로 저장
        T ret = _heap[0];

        // 마지막 데이터를 루트로 이동한다
        int lastIndex = _heap.Count - 1;
        _heap[0] = _heap[lastIndex];
        _heap.RemoveAt(lastIndex);
        lastIndex--;

        // 역으로 내려가는 도장깨기 시작
        int now = 0;
        while (true)
        {
            int left = 2 * now + 1;
            int right = 2 * now + 2;

            int next = now;
            // 왼쪽값이 현재값보다 크면, 왼쪽으로 이동
            if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                next = left;
            // 오른값이 현재값(왼쪽 이동 포함)보다 크면, 오른쪽으로 이동
            if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                next = right;

            // 왼쪽/오른쪽 모두 현재값보다 작으면 종료
            if (next == now)
                break;

            // 두 값을 교체한다
            (_heap[now], _heap[next]) = (_heap[next], _heap[now]);
            // 검사 위치를 이동한다
            now = next;
        }

        return ret;
    }

    public int Count { get { return _heap.Count; } }
}