﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using YsProject.Models;
using YsProject.Utility;

namespace YsProject.Utils
{
    public class EntityDao : IDisposable
    {
        #region Common

        /// <summary>
        /// DbContext
        /// </summary>
        private EFCoreDbContext _db;

        /// <summary>
        /// 事務
        /// </summary>
        private DbContextTransaction _trans;

        /// <summary>
        /// Roll Back
        /// </summary>
        private bool _rollBack;

        /// <summary>
        /// 接続日時
        /// </summary>
        public DateTime DbConectTime { get; set; }

        /// <summary>
        /// 初期化
        /// </summary>
        public EntityDao(bool transFlg = false)
        {
            _db = new EFCoreDbContext();

            // 事務設定
            if (transFlg)
            {
                this.DbConectTime = DateTime.Now;
                _trans = _db.Database.BeginTransaction();
                _rollBack = false;
            }
        }

        /// <summary>
        /// Roll Back
        /// </summary>
        public void RollBack()
        {
            _rollBack = true;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            // Roll Back判定
            if (_trans != null)
            {
                if (_rollBack) _trans.Rollback();
                else _trans.Commit();

                _trans.Dispose();
            }

            // Close
            _db.Dispose();
        }

        #endregion

        #region Db Function

        /// <summary>
        /// 件数取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int GetCount<T>() where T : class
            => _db.Set<T>().AsNoTracking().Count();

        /// <summary>
        /// 件数取得(WHEREある)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int GetCount<T>(Expression<Func<T, bool>> where = null) where T : class
             => where == null ? this.GetCount<T>() : _db.Set<T>().AsNoTracking().Where(where).Count();

        /// <summary>
        /// Keyで検索(Track)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public T FindTrack<T>(params object[] keys) where T : class
            => _db.Set<T>().Find(keys);

        /// <summary>
        /// Whereで唯一検索(NoTrack)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public T Find<T>(Expression<Func<T, bool>> where) where T : class
            => _db.Set<T>().AsNoTracking().Where(where).FirstOrDefault();

        /// <summary>
        /// Whereで唯一検索(Track)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public T FindTrack<T>(Expression<Func<T, bool>> where) where T : class
            => _db.Set<T>().Where(where).FirstOrDefault();

        /// <summary>
        /// Whereで複数検索(NoTrack)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> FindAll<T>() where T : class
            => _db.Set<T>().AsNoTracking().ToList();

        /// <summary>
        /// Whereで複数検索(NoTrack)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> FindAll<T>(Expression<Func<T, bool>> where) where T : class
            => where == null ? _db.Set<T>().AsNoTracking().ToList() : _db.Set<T>().AsNoTracking().Where(where).ToList();

        /// <summary>
        /// Whereで複数検索(NoTrack)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> FindAll<T, TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order = null, Expression<Func<int>> skip = null, Expression<Func<int>> take = null) where T : class
            => _db.Set<T>().AsNoTracking().SetWhere(where).SetOrder(order).SetSkip(skip).SetTake(take).ToList();

        /// <summary>
        /// Whereで複数検索(NoTrack)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<M> FindAll<T, M, TKey>(Expression<Func<T, M>> model, Expression<Func<T, bool>> where = null, Expression<Func<T, TKey>> order = null, Expression<Func<int>> skip = null, Expression<Func<int>> take = null) where T : class
            => _db.Set<T>().AsNoTracking().SetWhere(where).SetOrder(order).SetSkip(skip).SetTake(take).Select(model).ToList();

        /// <summary>
        /// Whereで複数検索(NoTrack)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> FindAll<T, TKey>(Expression<Func<T, bool>> where, List<Expression<Func<T, TKey>>> list = null, Expression<Func<int>> skip = null, Expression<Func<int>> take = null) where T : class
            => _db.Set<T>().AsNoTracking().SetWhere(where).SetOrder(list).SetSkip(skip).SetTake(take).ToList();

        /// <summary>
        /// Whereで複数検索(NoTrack)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<M> FindAll<T, M, TKey>(Expression<Func<T, M>> model, Expression<Func<T, bool>> where = null, List<Expression<Func<T, TKey>>> list = null, Expression<Func<int>> skip = null, Expression<Func<int>> take = null) where T : class
            => _db.Set<T>().AsNoTracking().SetWhere(where).SetOrder(list).SetSkip(skip).SetTake(take).Select(model).ToList();

        // <summary>
        /// Whereで複数検索(Track)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> FindAllTrack<T>(Expression<Func<T, bool>> where) where T : class
            => where == null ? _db.Set<T>().ToList() : _db.Set<T>().Where(where).ToList();

        /// <summary>
        /// SQLで複数検索(NoTrack)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramArray"></param>
        /// <returns></returns>
        public List<T> FindAll<T>(string sql) where T : class
            => _db.Database.SqlQuery<T>(sql).ToList();

        /// <summary>
        /// SQLで複数検索(NoTrack)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramArray"></param>
        /// <returns></returns>
        public List<T> FindAllLimit<T>(string sql, string gamenId) where T : class
            => _db.Database.SqlQuery<T>(sql + CommonUtil.getItemMaxCount(gamenId)).ToList();

        /// <summary>
        /// SQLで複数検索(NoTrack)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramArray"></param>
        /// <returns></returns>
        public List<T> FindAll<T>(string sql, List<MySqlParameter> paramArray) where T : class
            => _db.Database.SqlQuery<T>(sql, paramArray.ToArray()).ToList();

        /// <summary>
        /// SQLで複数検索(NoTrack)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramArray"></param>
        /// <returns></returns>
        public List<T> FindAll<T>(string sql, Func<T, T> model, List<MySqlParameter> paramArray = null) where T : class
            => _db.Database.SqlQuery<T>(sql, paramArray).Select(model).ToList();

        public T FindSingle<T>(string sql)
            => _db.Database.SqlQuery<T>(sql).Single();

        public T FindSingle<T>(string sql, List<MySqlParameter> paramArray = null)
            => _db.Database.SqlQuery<T>(sql, paramArray == null ? null : paramArray.ToArray()).Single();

        /// <summary>
        /// 追加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add<T>(T model) where T : class
            => TryCatchUpdateConcurrencyException(() => _db.Set<T>().Add(model));

        /// <summary>
        /// 追加List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add<T>(List<T> list) where T : class
            => TryCatchUpdateConcurrencyException(() => _db.Set<T>().AddRange(list));

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update<T>(T table) where T : class
            => TryCatchUpdateConcurrencyException(() => _db.Entry<T>(table).State = EntityState.Modified);

        /// <summary>
        /// 更新List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update<T>(List<T> list) where T : class
            => TryCatchUpdateConcurrencyException(() => list.ForEach(t => _db.Entry<T>(t).State = EntityState.Modified));

        /// <summary>
        /// 更新(複数)
        /// </summary>
        /// <param name="model"></param>
        //public void Update<T, M>(BaseModel model, List<M> list, Action<T, M> action) where T : class
        //{
        //    // 更新リスト
        //    List<T> updList = new List<T>();

        //    foreach (var item in list)
        //    {
        //        // WHERE 条件
        //        Expression<Func<T, bool>> where = this.GetUpdateWhereExpression<T, M>(item);

        //        // データ取得
        //        T data = this.FindTrack(where);

        //        if (data == null)
        //        {
        //            // 既に更新された(排他エラー)
        //            model.ErrorList.Add("既に更新された");
        //            this.RollBack();
        //            return;
        //        }
        //        else
        //        {
        //            // データ更新
        //            action(data, item);

        //            updList.Add(data);
        //        }
        //    }

        //    // 更新
        //    if (this.Update(updList) < 0)
        //    {
        //        // 更新中排他エラー
        //        model.ErrorList.Add("既に更新された");
        //        this.RollBack();
        //        return;
        //    }
        //}

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        //public void Update<T, M>(BaseModel model, M item, Action<T, M> action) where T : class
        //{
        //    // WHERE 条件
        //    Expression<Func<T, bool>> where = this.GetUpdateWhereExpression<T, M>(item);

        //    // データ取得
        //    T data = this.FindTrack(where);

        //    if (data == null)
        //    {
        //        // 既に更新された(排他エラー)
        //        model.ErrorList.Add("既に更新された");
        //        this.RollBack();
        //        return;
        //    }
        //    else
        //    {
        //        // データ更新
        //        action(data, item);
        //    }

        //    // 更新
        //    if (this.Update(data) < 0)
        //    {
        //        // 更新中排他エラー
        //        model.ErrorList.Add("既に更新された");
        //        this.RollBack();
        //        return;
        //    }
        //}

        /// <summary>
        /// GetWhereExpression
        /// </summary>
        /// <typeparam name="T">Table</typeparam>
        /// <typeparam name="M">Item</typeparam>
        /// <param name="item">Item</param>
        /// <returns></returns>
        private Expression<Func<T, bool>> GetUpdateWhereExpression<T, M>(M item) where T : class
        {
            // WHERE 条件
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            MemberExpression member1 = Expression.Property(parameter, "id");
            MemberExpression member2 = Expression.Property(parameter, "update_date");
            ConstantExpression constant1 = Expression.Constant(Convert.ToInt64(item.GetType().GetProperty("id").GetValue(item)));
            ConstantExpression constant2 = Expression.Constant(item.GetType().GetProperty("update_date").GetValue(item));
            return Expression.Lambda<Func<T, bool>>(Expression.And(Expression.Equal(member1, constant1), Expression.Equal(member2, constant2)), parameter);
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete<T>(T model) where T : class
            => TryCatchUpdateConcurrencyException(()
                => _db.Entry<T>(model).State = System.Data.Entity.EntityState.Deleted);


        public int DeleteAll(string sql)
            => _db.Database.ExecuteSqlCommand(sql);

        /// <summary>
        /// 削除List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Delete<T>(List<T> list) where T : class
        => TryCatchUpdateConcurrencyException(()
            => list.ForEach(model
                => _db.Entry<T>(model).State = System.Data.Entity.EntityState.Deleted));

        /// <summary>
        /// 排他確認
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private int TryCatchUpdateConcurrencyException(Action action)
        {
            try
            {
                // 変更処理
                action();
                // 保存
                return _db.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
            {
                // 排他発生
                _rollBack = true;
                return -1;
            }
        }

        #endregion
    }

    public static class EntityUtil
    {
        public static IQueryable<T> SetWhere<T>(this IQueryable<T> tb, Expression<Func<T, bool>> where = null) where T : class
            => where == null ? tb : tb.Where(where);

        public static IQueryable<T> SetOrder<T, TKey>(this IQueryable<T> tb, Expression<Func<T, TKey>> order = null) where T : class
            => order == null ? tb : tb.OrderBy(order);

        public static IQueryable<T> SetOrder<T, TKey>(this IQueryable<T> tb, List<Expression<Func<T, TKey>>> list = null) where T : class
        {
            if (list == null || list.Count <= 0)
            {
                return tb;
            }
            else
            {
                IOrderedQueryable<T> newTb = tb.OrderBy(list[0]);
                if (list.Count > 1)
                {
                    for (int i = 1; i < list.Count; i++)
                        newTb = newTb.ThenBy(list[i]);
                }
                return newTb;
            }
        }

        public static IQueryable<T> SetSkip<T>(this IQueryable<T> tb, Expression<Func<int>> skip = null) where T : class
            => skip == null ? tb : tb.Skip(skip);

        public static IQueryable<T> SetTake<T>(this IQueryable<T> tb, Expression<Func<int>> take = null) where T : class
            => take == null ? tb : tb.Take(take);

        public static List<T> Clone<T>(this List<T> list) where T : ICloneable
            => list.Select(x => (T)x.Clone()).ToList();
    }

    public class EntityBuilder
    {
        private StringBuilder _sqlHead = new StringBuilder();
        private StringBuilder _sqlBody = new StringBuilder();

        public string SqlCnt
        {
            get
            {
                return "SELECT COUNT(1) " + _sqlBody.ToString();
            }
        }

        public override string ToString()
        {
            return _sqlHead.ToString() + "\r\n" + _sqlBody.ToString();
        }

        public void AppendHead(string sql)
        {
            _sqlHead.AppendLine(sql);
        }

        public void AppendBody(string sql)
        {
            _sqlBody.AppendLine(sql);
        }

        public void Clear()
        {
            ClearHead();
            ClearBody();
        }

        public void ClearHead()
        {
            _sqlHead.Clear();
        }

        public void ClearBody()
        {
            _sqlBody.Clear();
        }
    }
}