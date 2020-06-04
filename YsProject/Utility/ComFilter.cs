//using Kebin1.Consts;
//using System.Web.Mvc;

//namespace YsTool.Utility
//{
//    /// <summary>
//    /// 未処理異常発生
//    /// </summary>
//    public class UndoExceptionAttribute : FilterAttribute, IExceptionFilter
//    {
//        public void OnException(ExceptionContext filterContext)
//        {
//            // Log
//            Log4.WriteError(filterContext.GetLoginString(), filterContext.Exception);

//            // 画面遷移
//           filterContext.HttpContext.Response.Redirect("~/Error/Error", true);
//        }
//    }

//    /// <summary>
//    /// Action開始終了イベント
//    /// </summary>
//    public class KebinActionAttribute : FilterAttribute, IActionFilter
//    {
//        public void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            // Log
//            Log4.WriteInfo(filterContext.GetLoginString() + "処理開始");

//            // Session存在判断 (ログイン画面無視)
//            if (filterContext.HttpContext.Session["SessionId"] == null && filterContext.RouteData.Values["controller"].ToString() != ViewCode.KEBIN10001.ToString())
//            {
//                // Log
//                Log4.WriteInfo(filterContext.GetLoginString() + "Session Out");

//                // ログイン画面へ遷移
//                //filterContext.HttpContext.Response.Redirect("~/KEBIN10001/KEBIN10001View", true);
//                //filterContext.Result = new ContentResult()
//                //{
//                //    Content = "{code:-1,message:'Session Out'}"
//                //};
//            }
//        }

//        public void OnActionExecuted(ActionExecutedContext filterContext)
//        {
//            // Log
//            Log4.WriteInfo(filterContext.GetLoginString() + "処理終了");
//        }
//    }

//    public static class ComFilter
//    {
//        public static string GetLoginString(this ControllerContext filterContext)
//            => "[" + (filterContext.HttpContext.Session["SessionId"] ?? "").ToString().Trim() + " " + (filterContext.HttpContext.Session["SessionName"] ?? "").ToString().Trim() + "] " +
//               "(" + filterContext.RouteData.Values["controller"] + " - " + filterContext.RouteData.Values["action"].ToString().PadRight(14, ' ') + ") ";
//    }
//}